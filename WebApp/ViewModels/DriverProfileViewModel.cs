using System;
using System.Collections.Generic;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;
using X.PagedList;

namespace WebApp.ViewModels
{
    public class DriverProfileViewModel
    {
        public ApplicationUserViewModel ApplicationUserViewModel {get ;set; }
        public List<RatesAndCommentsViewModel> RatesAndCommentList {get; set ;}
        public IPagedList<RatesAndCommentsViewModel> PagedRatesAndCommentList { get; set; }

        public float DrivingSafetyAverage { get; set; }
        public float PersonalCultureAverage { get; set; }
        public float PunctualityAverage { get; set; }
        public int NumberOfVotes { get; set; }

        public void SetListOfRatesAndComments(List<RatesAndComment> list, IApplicationUserRepository repository)
        {
            RatesAndCommentList = new List<RatesAndCommentsViewModel>();

            foreach(RatesAndComment rac in list)
            {
                RatesAndCommentList.Add(new RatesAndCommentsViewModel {
                    Comment = rac.Comment,
                    Date = rac.Date,
                    DrivingSafety = rac.DrivingSafety,
                    PersonalCulture = rac.PersonalCulture,
                    Punctuality = rac.Punctuality,
                    Username = repository.GetById(rac.UserId).UserName
                });
            }
        }
        public void SetAverages()
        {
            this.NumberOfVotes = RatesAndCommentList.Count;

            if (NumberOfVotes == 0)
            {
                DrivingSafetyAverage = 0;
                PersonalCultureAverage = 0;
                PunctualityAverage = 0;
            }
            else
            {
                foreach (var rat in RatesAndCommentList)
                {
                    DrivingSafetyAverage += rat.DrivingSafety;
                    PersonalCultureAverage += rat.PersonalCulture;
                    PunctualityAverage += rat.Punctuality;
                }

                DrivingSafetyAverage /= NumberOfVotes;
                PersonalCultureAverage /= NumberOfVotes;
                PunctualityAverage /= NumberOfVotes;

                Math.Round(DrivingSafetyAverage, 2);
                Math.Round(PersonalCultureAverage, 2);
                Math.Round(PunctualityAverage, 2);
            }
        }
    }
}

