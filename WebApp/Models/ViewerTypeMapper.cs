using System;
using WebApp.Data;

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
            if (int.Parse(user.Id) == tripDetails.DriverId)
                return ViewerType.Driver;

            if (tripDetails.Passangers!=null && tripDetails.Passangers.Contains(user))
                return ViewerType.Passanger;

            return ViewerType.Guest;
        }
    }
}
