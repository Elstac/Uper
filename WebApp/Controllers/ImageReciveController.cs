using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class ImageReciveController : Controller
    {
        IImageSaver imageSaver;

        public ImageReciveController(IImageSaver imageSaver)
        {
            this.imageSaver = imageSaver;
        }

        [HttpPost]
        public void Recive([FromBody]ImageObject @object)
        {
            var id = imageSaver.SaveImage(@object.imageData, ".png", "wwwroot/images/maps");
        }
    }

    public class ImageObject
    {
        public string imageData { get; set; }
    }
}
