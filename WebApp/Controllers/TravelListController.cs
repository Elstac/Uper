using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.ViewModels;
using X.PagedList;

namespace WebApp.Controllers
{
    public class TravelListController : Controller
    {
        ITripDetailsRepository repository;

        public TravelListController(ITripDetailsRepository repository)
        {
            this.repository = repository;
        }
      
        public IActionResult Index(string StartCity,string DestCity,DateTime MinDate, DateTime MaxDate,float Cost,bool Smoking,int? page,string SearchString)
        {
            #region Viewbagowanie
            if(SearchString != null)
            {
                ViewBag.StartCity = StartCity;
                ViewBag.DestCity = DestCity;
                ViewBag.MinDate = MinDate;
                ViewBag.MaxDate = MaxDate;
                ViewBag.Cost = Cost;
                ViewBag.Smoking = Smoking;
            }
            #endregion

            page = page ?? 1;

            if (String.IsNullOrEmpty(SearchString))
                return View(null);

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
            var List = repository.GetList(Specification);

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(List.ToPagedList(pageNumber,pageSize));
        }
    }
}