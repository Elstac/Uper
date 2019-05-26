using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Models
{
    public interface IViewerTypeMapper
    {
        ViewerType GetViewerType(ApplicationUser user, TripDetails tripDetails);
    }

    public class ViewerTypeMapper : IViewerTypeMapper
    {
        public ViewerType GetViewerType(ApplicationUser user, TripDetails tripDetails)
        {
            if (user == null)
                return ViewerType.Guest;

            if (user.Id == tripDetails.DriverId)
                return ViewerType.Driver;

            if (tripDetails.Passangers!=null)
            {
                foreach (var pass in tripDetails.Passangers)
                {
                    if (pass.User == user && pass.Accepted == true)
                        return ViewerType.Passanger;
                }
            }

            return ViewerType.Guest;
        }
    }
}
