using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Rating { get; set; }
        public int NumOfVotes { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
