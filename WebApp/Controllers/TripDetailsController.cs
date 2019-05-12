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

        public TripDetailsController(
            ITripDetailsViewModelProvider generator,
            IAccountManager accountManager,
            ITripUserRepository tripUserRepository,
            ITripDetailsRepository tripDetailsRepository,
            IViewerTypeMapper viewerTypeMapper, 
            IApplicationUserRepository applicationUserRepository,
            IFileReader<string> fileReader,
            IFileManagerFactory fileManagerFactory)
        {
            this.generator = generator;
            this.accountManager = accountManager;
            this.tripUserRepository = tripUserRepository;
            this.tripDetailsRepository = tripDetailsRepository;
            this.viewerTypeMapper = viewerTypeMapper;
            this.applicationUserRepository = applicationUserRepository;
            this.fileReader = fileReader;
            fileManager = fileManagerFactory.GetManager(FileType.Json);
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

            if (vm.DateEnd.CompareTo(DateTime.Now) <= 0) ViewBag.IsActive = "false";
            else if(vm.DateEnd.CompareTo(DateTime.Now) > 0 && vm.Date.CompareTo(DateTime.Now) <= 0)ViewBag.IsActive = "ongoing";
            else ViewBag.IsActive ="true";

            if (vm.PassangersUsernames != null)
            {
                if (vm.PassangersUsernames.Contains(user.UserName))
                {
                    ViewBag.PassangerAccepted = true;
                }
                else ViewBag.PassangerAccepted = false;
            }
            else ViewBag.PassangerAccepted = false;


            ViewData["type"] = viewerType;
            if (vm.MapPath != null)
                ViewData["mapData"] = fileReader.ReadFileContent("wwwroot"+vm.MapPath);



            return View(vm);
        }

        [Authorize(Policy = "test")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(int id)
        {
            TripUser tripUser = new TripUser {
                TripId = id,
                UserId = accountManager.GetUserId(HttpContext.User)
            };

            tripUserRepository.Add(tripUser);
            return RedirectToAction("index", "TripDetails", new { id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var td = tripDetailsRepository.GetById(id);
            if (td.MapId != null)
            {
                fileManager.RemoveFile(td.MapId, "wwwroot/images/maps/");
            }
            tripUserRepository.RemoveTripUsers(id);
            tripDetailsRepository.Remove(td);
            return RedirectToAction("index", "TripDetails", new { id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Leave(int id)
        {
            tripUserRepository.RemoveUserFromTrip(id,accountManager.GetUserId(HttpContext.User));
            return RedirectToAction("index", "TripDetails", new { id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUserFromTrip(int id,string username)
        {
            List<TripUser> toRm = tripUserRepository.GetList(new TripUserByUsernameAndTripId(id,username)) as List<TripUser>;
            tripUserRepository.Remove(toRm[0]);
            return RedirectToAction("index", "TripDetails", new { id });

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmRequest(int tripId, string username)
        {
            var tu = tripUserRepository.GetList(new TripUserByUsernameAndTripId(tripId,username)) as List<TripUser>;

            if (tu == null)
                return BadRequest(new { error = "invalid user ot trip id" });

            tu[0].Accepted = true;
            tripUserRepository.Update(tu[0]);

            return RedirectToAction("index", "TripDetails", new { id = tripId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult GeneratePdf(int tripId)
        {
            var vm = generator.GetViewModel(tripId, (ViewerType)0);

            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page to the document.
            PdfPage page = doc.Pages.Add();
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;
            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Draw the text
            graphics.DrawString(vm.DestinationAddress.City, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.DestinationAddress.Street, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.StartingAddress.City, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.StartingAddress.Street, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.Date.ToString(), font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.DateEnd.ToString(), font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.Cost.ToString(), font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.VechicleModel, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.Size.ToString(), font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.IsSmokingAllowed.ToString(), font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString(vm.Description, font, PdfBrushes.Black, new PointF(0, 0));
            //Load the image as stream.
            FileStream imageStream = new FileStream(fileReader.ReadFileContent("wwwroot" + vm.MapPath), FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            graphics.DrawImage(image, 0, 0);
            //Save the PDF document to stream
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            //string fileName = "Output.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType);//, fileName);
        }
    }
}
