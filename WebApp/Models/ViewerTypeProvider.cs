using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data.Repositories;

namespace WebApp.Models
{
    public interface IViewerTypeProvider
    {
        ViewerType GetViewerType(string userId,int tripId);
        Task<ViewerType> GetViewerTypeAsync(ClaimsPrincipal userClaims, int tripId);
    }

    public class ViewerTypeProvider : IViewerTypeProvider
    {
        private IViewerTypeMapper typeMapper;
        private IAccountManager accountManager;
        private ITripDetailsRepository tripRepository;
        private IApplicationUserRepository userRepository;

        public ViewerTypeProvider(
            IViewerTypeMapper typeMapper,
            IAccountManager accountManager,
            ITripDetailsRepository tripRepository,
            IApplicationUserRepository userRepository)
        {
            this.typeMapper = typeMapper;
            this.accountManager = accountManager;
            this.tripRepository = tripRepository;
            this.userRepository = userRepository;
        }

        public ViewerType GetViewerType(string userId, int tripId)
        {
            var user = userRepository.GetById(userId);
            var trip = tripRepository.GetTripWithPassengersById(tripId);

            return typeMapper.GetViewerType(user, trip);
        }

        public async Task<ViewerType> GetViewerTypeAsync(ClaimsPrincipal userClaims, int tripId)
        {
            var trip = tripRepository.GetTripWithPassengersById(tripId);
            var user = await accountManager.GetUserAsync(userClaims);

            return typeMapper.GetViewerType(user, trip);
        }
    }
}
