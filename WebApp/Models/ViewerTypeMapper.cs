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
                var list = (List<TripUser>)(tripDetails.Passangers);
                if (list.Exists(td => td.User == user))
                    return ViewerType.Passanger;
            }

            return ViewerType.Guest;
        }
    }
}
