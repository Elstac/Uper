using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Models.OfferList;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TravelListController : Controller
    {
        IListCreator listCreator;

        public TravelListController(IListCreator listCreator)
        {
            this.listCreator = listCreator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string StartCity,string DestCity,DateTime MinDate, DateTime MaxDate,float Cost,int? page)
        {
            page = page ?? 1;
            IQueryable < TripDetails > List = listCreator.GetList(Cost, StartCity, DestCity,MinDate,MaxDate);
            TravelListViewModel vm = new TravelListViewModel(List);
            return View(vm);
        }
    }
}