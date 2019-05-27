using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Specifications;

namespace WebApp.Models
{
    public interface ITripTimeCollisionChecker
    {
        bool IsColliding(int tripId, string userId);
        bool IsColliding(string userId, DateTime dateStart, DateTime dateEnd);
    }

    public class TripTimeCollisionChecker : ITripTimeCollisionChecker
    {
        private ITripDetailsRepository tripDetailsRepository;
        public TripTimeCollisionChecker
            (
            ITripDetailsRepository tripDetailsRepository
            )
        {
            this.tripDetailsRepository = tripDetailsRepository;
        }
        public bool IsColliding(int tripId,string userId)
        {
            //Get trip that user try to join
            var actualTrip = tripDetailsRepository.GetById(tripId);
            //Check user driver trips
            var userTrips = tripDetailsRepository.GetList(new CollidingDriverTrips(userId, actualTrip.Date, actualTrip.DateEnd)).ToList();
            //Check if his trips collide with trip he try to join
            if (userTrips != null)
            {
                if (userTrips.Count > 0) return true;
            }
            //Get colliding trips where he is passanger
            var userJoinedCollidingTrips = tripDetailsRepository.GetList(new CollidingJoinedUserTrips(userId, actualTrip.Date, actualTrip.DateEnd)).ToList();
            //Check if trips are colliding
            if (userJoinedCollidingTrips != null)
            {
                if (userJoinedCollidingTrips.Count > 0) return true;
            }

            return false;

        }

        public bool IsColliding(string userId,DateTime dateStart, DateTime dateEnd)
        {
            //Check user driver trips
            var userTrips = tripDetailsRepository.GetList(new CollidingDriverTrips(userId, dateStart, dateEnd)).ToList();
            //Check if his trips collide with trip he try to join
            if (userTrips != null)
            {
                if (userTrips.Count > 0) return true;
            }

            //Get colliding trips where he is passanger
            var userJoinedCollidingTrips = tripDetailsRepository.GetList(new CollidingJoinedUserTrips(userId, dateStart, dateEnd)).ToList();
            //Check if trips are colliding
            if (userJoinedCollidingTrips != null)
            {
                if (userJoinedCollidingTrips.Count > 0) return true;
            }

            return false;
        }
    }
}
