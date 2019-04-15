using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationContext mContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        public HomeController(ApplicationContext context)
        {
            mContext = context;
        }
        public IActionResult Index()
        {
            // Make sure we have the database
            mContext.Database.EnsureCreated();

            return View();
        }

        public void Test()
        {


        }
    }
}
