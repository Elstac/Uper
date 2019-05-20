using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;
using WebApp.Models;
using System.Linq.Expressions;
using WebApp.Data.Specifications;
using System.Collections.Generic;
using WebApp.Models.FileManagement;
using System;
using WebApp.Models.Factories;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using Syncfusion.Drawing;
using WebApp.Models.HtmlNotifications;
using WebApp.Models.TravellChangeEmail;
using System.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private ITripDetailsViewModelProvider generator;
        private IAccountManager accountManager;
        private ITripUserRepository tripUserRepository;
        private ITripDetailsRepository tripDetailsRepository;
        private IViewerTypeMapper viewerTypeMapper;
        private IApplicationUserRepository applicationUserRepository;
        private IFileReader<string> fileReader;
        private IFileManager fileManager;
        private IFileManager pngFileManager;
        private IPdfCreator pdfCreator;
        private INotificationProvider notificationProvider;
        private IOfferStateEmailSender stateEmailSender;

        public TripDetailsController(
            ITripDetailsViewModelProvider generator,
            IAccountManager accountManager,
            ITripUserRepository tripUserRepository,
            ITripDetailsRepository tripDetailsRepository,
            IViewerTypeMapper viewerTypeMapper, 
            IApplicationUserRepository applicationUserRepository,
            IFileReader<string> fileReader,
            IFileManagerFactory fileManagerFactory,
            IPdfCreator pdfCreator,
            INotificationProvider notificationProvider,
            IOfferStateEmailSender stateEmailSender)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.tripUserRepository = tripUserRepository;
            this.tripDetailsRepository = tripDetailsRepository;
            this.viewerTypeMapper = viewerTypeMapper;
            this.applicationUserRepository = applicationUserRepository;
            this.fileReader = fileReader;
            this.notificationProvider = notificationProvider;
            this.stateEmailSender = stateEmailSender;

            fileManager = fileManagerFactory.GetManager(FileType.Json);
            pngFileManager = fileManagerFactory.GetManager(FileType.Png);
            this.pdfCreator = pdfCreator;
        }

        /// <summary>
        /// Get details page about trip with <paramref name="id"/>. Content depends on <paramref name="viewerType"/> of viewer.
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns>Details page</returns>
        public IActionResult Index(int id)
        {
            #region Getting ViewerType
            var userid = accountManager.GetUserId(HttpContext.User);
            var user = applicationUserRepository.GetById(userid);
            var data = tripDetailsRepository.GetTripWithPassengersById(id);
            var viewerType = viewerTypeMapper.GetViewerType(user, data);
            #endregion

            var vm = generator.GetViewModel(id, viewerType);

            if (vm.PassangersUsernames != null)
            {
                if (vm.PassangersUsernames.Contains(user.UserName))
                {
                    ViewBag.PassangerAccepted = true;
                }
                else ViewBag.PassangerAccepted = false;
            }
            else ViewBag.PassangerAccepted = false;

            if(vm.DriverUsername == null)
            {
                vm.DriverUsername = applicationUserRepository.GetById(vm.DriverId).UserName;
            }

            ViewData["type"] = viewerType;
            if (vm.MapPath != null)
                ViewData["mapData"] = fileReader.ReadFileContent("wwwroot"+vm.MapPath);



            return View(vm);
        }

        [Authorize(Policy = "test")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(int tripId)
        {
            TripUser tripUser = new TripUser {
                TripId = tripId,
                UserId = accountManager.GetUserId(HttpContext.User)
            };

            tripUserRepository.Add(tripUser);
            return RedirectToAction("index", "TripDetails", new { id = tripId });
        }

        [Authorize(Policy = "DriverOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int tripId)
        {
            var td = tripDetailsRepository.GetById(tripId);
            if (td.MapId != null)
            {
                fileManager.RemoveFile(td.MapId, "wwwroot/images/maps/");
            }

            var passengers = from user in td.Passangers
                             select user.User;

            stateEmailSender.SendOfferStateChangedEmail(passengers, GetDetailsURL(tripId), OfferStateChange.Deleted);

            tripUserRepository.RemoveTripUsers(tripId);
            tripDetailsRepository.Remove(td);

            notificationProvider.SetNotification(HttpContext.Session, "res-suc", "Trip deleted successfully");
            return RedirectToRoute("Home");
        }

        [Authorize(Policy ="PassangerOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Leave(int tripId)
        {
            tripUserRepository.RemoveUserFromTrip(tripId,accountManager.GetUserId(HttpContext.User));
            return RedirectToAction("index", "TripDetails", new { id = tripId });
        }

        [Authorize(Policy = "DriverOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUserFromTrip(int tripId,string username)
        {
            List<TripUser> toRm = tripUserRepository.GetList(new TripUserByUsernameAndTripId(tripId,username)) as List<TripUser>;

            stateEmailSender.SendOfferStateChangedEmail(toRm[0].User, GetDetailsURL(tripId), OfferStateChange.UserRemoved);

            tripUserRepository.Remove(toRm[0]);
            return RedirectToAction("index", "TripDetails", new { id = tripId });
        }

        [Authorize(Policy = "DriverOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmRequest(int tripId, string username)
        {
            var tu = tripUserRepository.GetList(new TripUserByUsernameAndTripId(tripId, username)) as List<TripUser>;

            if (tu == null)
                return BadRequest(new { error = "invalid user ot trip id" });

            tu[0].Accepted = true;
            tripUserRepository.Update(tu[0]);

            stateEmailSender.SendOfferStateChangedEmail(tu[0].User, GetDetailsURL(tripId), OfferStateChange.RequestAccepted);

            return RedirectToAction("index", "TripDetails", new { id = tripId });
        }

        private string GetDetailsURL(int tripId)
        {
            return Url.Action("Index", "TripDetails", new { id = tripId}, Request.Scheme);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GeneratePdf(int tripId,string generatepdf)
        {
            var vm = tripDetailsRepository.GetById(tripId);
            
            MemoryStream stream = pdfCreator.CreatePdf(vm, generatepdf);

            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType);
        }
    }
}
