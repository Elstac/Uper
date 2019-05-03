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
            if (MaxDate != null && MinDate != null && MaxDate >= MinDate)
                return Content("Wrong Date");

            #region Viewbaging
                ViewBag.StartCity = StartCity;
                ViewBag.DestCity = DestCity;
                ViewBag.MinDate = MinDate;
                ViewBag.MaxDate = MaxDate;
                ViewBag.Cost = Cost;
                ViewBag.Smoking = Smoking;
            #endregion

            //if there is no input display all travel offers
            #region EmptyInput/ListAllTrips
            if (String.IsNullOrWhiteSpace(StartCity) && 
                String.IsNullOrWhiteSpace(DestCity) 
                && MinDate == null 
                && MaxDate == null 
                && Cost == null)
            {
                var _List = repository.GetAll();

                page = page ?? 1;
                int _pageSize = 10;
                int _pageNumber = (page ?? 1);
                return View(_List.ToPagedList(_pageNumber, _pageSize));
            }         
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