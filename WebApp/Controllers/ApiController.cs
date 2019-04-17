using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        ITripDetailsRepository repository;

        public ApiController(ITripDetailsRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TripDetails> Get()
        {
            return repository.GetList(new ApiTripDetailsSpecification(20));
        }
    }
}
