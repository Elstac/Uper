using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Exceptions;

namespace WebApp.Models.EmailConfirmation
{
    /// <summary>
    /// Provides confirmation of general operation requiring email confirmation
    /// </summary>
    public interface IEmailConfirmator
    {
        /// <summary>
        /// Provides confirmation of general operation requiring email confirmation for user defined
        /// by <paramref name="Id"/>, using <paramref name="token"/> and additional <paramref name="parameters"/>
        /// if neccesery for operation.
        /// </summary>  
        Task ConfirmEmailAsync(string Id, string token, params object[] parameters);
    }

    public class EmailConfirmator : IEmailConfirmator
    {
        private IConfirmationProvider confirmBehavior;
        private IApplicationUserRepository userRepository;

        /// <summary>
        /// Base implementation of EmailConfirmator
        /// </summary>
        /// <param name="confirmBehavior">Confirmation provider for confirmation operation</param>
        /// <param name="userRepository">Repository of users in application</param>
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
