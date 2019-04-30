using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class ImageReciveController : Controller
    {
        [HttpPost]
        public void Recive([FromBody]ImageObject @object)
        {
            
        }
    }

    public class ImageObject
    {
        public string imageData { get; set; }
    }
}
