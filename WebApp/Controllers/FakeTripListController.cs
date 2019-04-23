using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FakeTripListController : Controller
    {
        private readonly ApplicationContext _context;
        protected ITripDetailsRepository tripDetailsRepository;
        private ITripDetailsViewModelGenerator generator;

        public FakeTripListController(ApplicationContext context, ITripDetailsRepository _tripDetailsRepository, ITripDetailsViewModelGenerator _generator)
        {
            _context = context;
            tripDetailsRepository = _tripDetailsRepository;
            generator = _generator;
        }

        // GET: FakeTripList
        public IActionResult Index()
        {

            return View(tripDetailsRepository.GetAll());
        }
        
    }
       
}
