using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TripCreatorController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(TripCreatorViewModel model)
        {
            string message = "";

            if (ModelState.IsValid && model.Cost >= 0.0)
            {
                //TO DO 
                //Add 
                //model.DriverId = 
                message = $"Miasto koncowe = {model.DestinationAddress.City}\n";
                message += $"Ulica koncowa = { model.DestinationAddress.Street}";
                message += $"Miasto poczatkowe = { model.StartingAddress.Street}";
                message += $"Ulica poczatkowa = { model.StartingAddress.Street}";
                message += $"Koszt = { model.Cost}";
                message += $"Samochod = { model.VechicleModel}";
                message += $"Data = { model.Date}";
                message += $"Opis = { model.Description}";

            }
            else
            {
                message = "Failure in creating trip. Please try again";
            }
            return Content(message);
        }
    }
}