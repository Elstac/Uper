using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class TripDetailsController : Controller
    {
        private ITripDetailsViewModelProvider generator;

        public TripDetailsController(ITripDetailsViewModelProvider generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Get details page about trip with <paramref name="id"/>. Content depends on <paramref name="viewerType"/> of viewer.
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns>Details page</returns>
        public IActionResult Index([FromQuery]int id, [FromQuery]ViewerType viewerType)
        {
            var vm = generator.GetViewModel(id,viewerType);
            
            ViewData["type"] = viewerType;
            return View(vm);
        }
        
    }
}
