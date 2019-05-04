using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
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
        /// <param name="Smoking">Smoker or non-Smoker</param>
        /// <param name="page">current page</param>
        public IActionResult Index(string StartCity,string DestCity,DateTime? MinDate, DateTime? MaxDate,float? Cost,bool Smoking,int? page)
        {
            if (MaxDate != null && MinDate != null && MaxDate <= MinDate)
                return Content("Wrong Date");

            if (Cost != null && Cost < 0)
            if (Cost != null && Cost < 0)
                return Content("Wrong Cost");

            #region Viewbaging
            ViewBag.StartCity = StartCity;
                ViewBag.DestCity = DestCity;
                ViewBag.MinDate = MinDate;
                ViewBag.MaxDate = MaxDate;
                ViewBag.Cost = Cost;
                ViewBag.Smoking = Smoking;
            #endregion
            
            //if page is null it's sets it as 1
            //page starts at null when 'Search' is pressed
            page = page ?? 1;
            
            ISpecification<TripDetails> Specification = new TravelListSpecification(StartCity, DestCity, MinDate, MaxDate, Cost, Smoking);
            var List = repository.GetList(Specification);
            
            // the number of elements displayed on a single page of list
            int pageSize = 10;
            // the number of a page currently displayed
            int pageNumber = (page ?? 1);
            return View(List.ToPagedList(pageNumber,pageSize));
        }
    }
}