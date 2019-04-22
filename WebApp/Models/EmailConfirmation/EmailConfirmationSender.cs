using System.Threading.Tasks;
using WebApp.Data.Repositories;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public interface IEmailConfirmationSender
    {
        Task SendConfirmationEmailAsync(string Id, string url);
    }

    public class EmailConfirmatorSender:IEmailConfirmationSender
    {
        private IEmailService emailService;
        private IMessageBodyDictionary messageBody;
        private IApplicationUserRepository userRepository;
        private IConfirmationTokenProvider tokenProvider;
        private string messageType;

        /// <summary>
        /// Base implenmentation of EmailConfirmator
        /// </summary>
        /// <param name="emailService">Service used for sending emails</param>
        /// <param name="messageBody">Basse message body exluding link replacement</param>
        /// <param name="userRepository">Repository of users</param>
        /// <param name="tokenProvider">Token provider for confirmation</param>
        /// <param name="messageType">Name of message template used in email sender</param>
        public EmailConfirmatorSender(IEmailService emailService,
            IMessageBodyDictionary messageBody, 
            IApplicationUserRepository userRepository, 
            IConfirmationTokenProvider tokenProvider, 
            string messageType)
        {
            this.emailService = emailService;
            this.messageBody = messageBody;
            this.userRepository = userRepository;
            this.tokenProvider = tokenProvider;
            this.messageType = messageType;
        }

        public async Task SendConfirmationEmailAsync(string Id, string url)
        {
            var user = userRepository.GetById(Id);
            if (user == null)
                throw new OwnNullArgumentException("Invalid user id");

            var token = await tokenProvider.GenerateTokenAsync(user);

            messageBody.AddReplacement(url + $"?id={Id}&token={token}", "{Link}");

            emailService.SendMail("Uper", user.Email, messageType, messageBody);
        }
    }
}
