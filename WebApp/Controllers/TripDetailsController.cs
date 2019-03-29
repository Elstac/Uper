using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private ITripDetailsViewModelGenerator generator;

        public TripDetailsController(ITripDetailsViewModelGenerator generator)
        {
            this.generator = generator;
        }

        // GET: /<controller>/
        public IActionResult Index(int id, int type)
        {
            var vm = generator.GetViewModel(id, (ViewerType)type);
            
            return View(vm);
        }
        
        public IActionResult Test(int id,int type)
        {
            return Content($"Id: {id} Type: {type}", "text/html");
        }
    }
}
