using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    interface IEmailConfirmatorAsync
    {
        Task SendConfirmationEmailAsync(string Id, string url);
        Task ConfirmEmailAsync(string Id, string token, params object[] parameters);
    }

    public class EmailConfirmatorAsync : IEmailConfirmatorAsync
    {
        private IConfirmationProvider confirmAsyncBehavior;
        private IEmailService emailService;
        private IMessageBodyDictionary messageBody;
        private IApplicationUserRepository userRepository;
        private string messageType;

        public EmailConfirmatorAsync(IConfirmationProvider confirmAsyncBehavior,
                                     IEmailService emailService, 
                                     IMessageBodyDictionary messageBody,
                                     IApplicationUserRepository userRepository,
                                     string messageType)
        {
            this.confirmAsyncBehavior = confirmAsyncBehavior;
            this.emailService = emailService;
            this.messageBody = messageBody;
            this.userRepository = userRepository;
            this.messageBody = messageBody;
            this.messageType = messageType;
        }

        public async Task ConfirmEmailAsync(string Id, string token, params object[] parameters)
        {
            var user = userRepository.GetById(Id);

            if (user == null)
                throw new OwnNullArgumentException("Invalid user id");

            token = token.Replace(" ", "+");

            await confirmAsyncBehavior.ConfirmAsync(user, token, parameters);
        }

        public async Task SendConfirmationEmailAsync(string Id, string url)
        {
            var user = userRepository.GetById(Id);
            if (user == null)
                throw new OwnNullArgumentException("Invalid user id");

            var token = await confirmAsyncBehavior.GenerateTokenAsync(user);

            messageBody.AddReplacement(url + $"?id={Id}&token={token}", "{Link}");

            emailService.SendMail("Uper", user.Email, messageType, messageBody);
        }
    }
}
