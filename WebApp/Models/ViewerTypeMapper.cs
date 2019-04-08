using System;
using System.Security.Claims;
using WebApp.Data;

namespace WebApp.Models
{
    public interface IViewerTypeMapper
    {
        ViewerType GetViewerType(ClaimsPrincipal principal, TripDetails tripDetails);
    }

    public class ViewerTypeMapper : IViewerTypeMapper
    {
        private IAccountManager accountManager;

        public ViewerTypeMapper(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public ViewerType GetViewerType(ClaimsPrincipal user, TripDetails tripDetails)
        {
            var user = 
            if (int.Parse(user.Id) == tripDetails.DriverId)
                return ViewerType.Driver;

            if (tripDetails.Passangers!=null && tripDetails.Passangers.Contains(user))
                return ViewerType.Passanger;

            return ViewerType.Guest;
        }
    }
}
