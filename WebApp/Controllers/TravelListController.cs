using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;
using WebApp.Models.TravelList;
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
      
        public IActionResult Index(string StartCity,string DestCity,DateTime? MinDate, DateTime? MaxDate,float? Cost,bool Smoking,int? page,string SearchString)
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

            #region EmptyInput
            if(String.IsNullOrWhiteSpace(StartCity) && 
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

            page = page ?? 1;

            if (String.IsNullOrEmpty(SearchString))
                return View(null);

            Expression<Func<TripDetails, bool>> criteria = TravelListCriteriaProvider.GetCriteria(StartCity,DestCity, MinDate, MaxDate, Cost, Smoking);
            ISpecification<TripDetails> Specification = new TravelListSpecification(criteria);
            var List = repository.GetList(Specification);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(List.ToPagedList(pageNumber,pageSize));
        }
    }
}