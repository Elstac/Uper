using System;
using WebApp.Models.TripDetailViewModelProvider;

namespace WebApp.Models.Factories
{
    public interface ITripDetailsViewModelCreatorFactory
    {
        ITripDetailsCreator CreateCreator(ViewerType viewerType);
    }

    public class TripDetailViewModelCreatorFactory:ITripDetailsViewModelCreatorFactory
    {
        private IServiceProvider provider;

        public TripDetailViewModelCreatorFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Create base TripDetailsViewModelCreator and wrap it with decorators which depend on viewer type.
        /// </summary>
        /// <param name="viewerType">Type of viewer</param>
        /// <returns></returns>
        public ITripDetailsCreator CreateCreator(ViewerType viewerType)
        {
            var ret = (ITripDetailsCreator)provider.GetService(typeof(ITripDetailsCreator));

          // if (viewerType != ViewerType.Guest)
           // {
                ret = new PassengerListDecorator(ret);
           // }
            if (viewerType == ViewerType.Driver)
                ret = new RequestListDecorator(ret);

            ret = new MapDecorator(ret);

            return ret;
        }
    }
}
