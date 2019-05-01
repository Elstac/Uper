using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Models.OfferList;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TravelListController : Controller
    {
        ITripDetailsRepository repository;

        public TravelListController(ITripDetailsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string StartCity,string DestCity,DateTime MinDate, DateTime MaxDate,float Cost,bool Smoking,int? page)
        {
            page = page ?? 1;
            Expression<Func<TripDetails, bool>> criteria =
                c => c.Cost <= Cost && c.DestinationAddress.City.ToUpper() == DestCity.ToUpper() &&
                c.StartingAddress.City.ToUpper() == StartCity.ToUpper() &&
                c.Date >= MinDate &&
                c.Date <= MaxDate;
            if(Smoking)
            {
                var com = criteria.Compile();
                criteria = c => com(c) && c.IsSmokingAllowed == Smoking;
            }
            ISpecification<TripDetails> Specification = new TravelListSpecification(criteria);
            IQueryable < TripDetails > List = repository.GetList(Specification).AsQueryable();
            TravelListViewModel vm = new TravelListViewModel(List);
            return View(vm);
        }
    }
}