using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Exceptions;

namespace WebApp.Models.EmailConfirmation
{
    public interface IEmailConfirmator
    {
        Task ConfirmEmailAsync(string Id, string token, params object[] parameters);
    }

    public class EmailConfirmator : IEmailConfirmator
    {
        private IConfirmationProvider confirmBehavior;
        private IApplicationUserRepository userRepository;

        public EmailConfirmator(IConfirmationProvider confirmBehavior,
                                IApplicationUserRepository userRepository)
        {
            this.confirmBehavior = confirmBehavior;
            this.userRepository = userRepository;
        }

        public async Task ConfirmEmailAsync(string Id, string token, params object[] parameters)
        {
            var user = userRepository.GetById(Id);

            if (user == null)
                throw new OwnNullArgumentException("Invalid user id");

            token = token.Replace(" ", "+");

            await confirmBehavior.ConfirmAsync(user, token, parameters);
        }
    }
}
