using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private IApplicationUserViewModelGenerator generator;

        public ProfilesController(IApplicationUserViewModelGenerator generator)
        {
            this.generator = generator;
        }

        public IActionResult Index(int id)
        {
            var vm = generator.GetViewModel(id);

            return View(vm);
        }
    }
}