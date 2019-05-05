using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.ViewModels
{
    public class DriverProfileViewModel
    {
        public ApplicationUserViewModel ApplicationUserViewModel {get ;set; }
        public List<RatesAndComment> RatesAndCommentList {get; set ;}

        public float DrivingSafetyAverage { get; set; }
        public float PersonalCultureAverage { get; set; }
        public float PunctualityAverage { get; set; }
        public int NumberOfVotes { get; set; }

        public void SetAverages()
        {
            this.NumberOfVotes = RatesAndCommentList.Count;
            foreach(var rat in RatesAndCommentList)
            {
                DrivingSafetyAverage += rat.DrivingSafety;
                PersonalCultureAverage += rat.PersonalCulture;
                PunctualityAverage += rat.Punctuality;
            }

            DrivingSafetyAverage /= NumberOfVotes;
            PersonalCultureAverage /= NumberOfVotes;
            PunctualityAverage /= NumberOfVotes;
        }
    }
}

