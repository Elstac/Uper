using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Data
{
    [ComplexType]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
