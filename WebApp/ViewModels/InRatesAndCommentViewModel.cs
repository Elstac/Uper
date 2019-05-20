using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class InRatesAndCommentViewModel
    {
        [Required]
        [Range(0, 5)]
        public int DrivingSafety { get; set; }
        [Required]
        [Range(0, 5)]
        public int PersonalCulture { get; set; }
        [Required]
        [Range(0, 5)]
        public int Punctuality { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        [StringLength(500)]
        [Required]
        public string DriverId { get; set; }
        [Required]
        public int TripId { get; set; }
    }
}
