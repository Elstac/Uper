﻿using System;
using System.Collections.Generic;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;

namespace WebApp.ViewModels
{
    public class DriverProfileViewModel
    {
        public ApplicationUserViewModel ApplicationUserViewModel {get ;set; }
        public List<V2RatesAndCommentsViewModel> RatesAndCommentList {get; set ;}

        public float DrivingSafetyAverage { get; set; }
        public float PersonalCultureAverage { get; set; }
        public float PunctualityAverage { get; set; }
        public int NumberOfVotes { get; set; }

        public void SetListOfRatesAndComments(List<RatesAndComment> list, IApplicationUserRepository repository)
        {
            RatesAndCommentList = new List<V2RatesAndCommentsViewModel>();

            foreach(RatesAndComment rac in list)
            {
                RatesAndCommentList.Add(new V2RatesAndCommentsViewModel {
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

