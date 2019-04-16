using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Data
{
    [Owned]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
