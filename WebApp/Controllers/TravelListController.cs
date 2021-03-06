﻿using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult Index(
            string StartCity,
            string DestCity,
            DateTime? MinDate, 
            DateTime? MaxDate,
            float? Cost,
            bool Smoking,int?
            page,int? Seats)
        {
            if (MaxDate != null && MinDate != null && MaxDate <= MinDate)
                return Content("Wrong Date");

            if (Cost != null && Cost < 0)
                return Content("Wrong Cost");

            if (Seats != null && Seats < 1)
                return Content("Wrong Number of empty Seats");

            #region Viewbaging
            ViewBag.StartCity = StartCity;
            ViewBag.DestCity = DestCity;
            ViewBag.MinDate = MinDate;
            ViewBag.MaxDate = MaxDate;
            ViewBag.Cost = Cost;
            ViewBag.Smoking = Smoking;
            ViewBag.Seats = Seats;
            #endregion
            
            //if page is null it's sets it as 1
            //page starts at null when 'Search' is pressed
            page = page ?? 1;
            
            var List = repository.GetList(
                new TravelListSpecification(StartCity, DestCity, MinDate, MaxDate, Cost, Smoking, Seats));
            
            // the number of elements displayed on a single page of list
            int pageSize = 10;
            // the number of a page currently displayed
            int pageNumber = (page ?? 1);
            return View(List.ToPagedList(pageNumber,pageSize));
        }
    }
}