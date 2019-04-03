using System;

namespace WebApp.Models.Factories
{
    public interface ITripDetailViewModelCreatorFactory
    {
        ITripDetailsCreator CreateCreator(ViewerType viewerType);
    }

    public class TripDetailViewModelCreatorFactory:ITripDetailViewModelCreatorFactory
    {
        private IServiceProvider provider;

        public TripDetailViewModelCreatorFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ITripDetailsCreator CreateCreator(ViewerType viewerType)
        {
            var ret = (ITripDetailsCreator)provider.GetService(typeof(ITripDetailsCreator));

            if (viewerType != ViewerType.Guest)
            {
                ret = new PassengerListDecorator(ret);
            }

            return ret;
        }
    }
}
