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
            throw new NotImplementedException();
        }
    }
}
