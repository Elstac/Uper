using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class RatesAndCommentsViewModel
    {
        public int DrivingSafety { get; set; }
        public int PersonalCulture { get; set; }
        public int Punctuality { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}


