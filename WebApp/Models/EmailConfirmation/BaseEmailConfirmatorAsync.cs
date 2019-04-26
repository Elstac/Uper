using System;
using System.Threading.Tasks;
using WebApp.Data.Repositories;
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
        }

        public async Task ConfirmEmailAsync(string Id, string token, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task SendConfirmationEmailAsync(string Id, string url)
        {
            throw new NotImplementedException();
        }
    }
}
