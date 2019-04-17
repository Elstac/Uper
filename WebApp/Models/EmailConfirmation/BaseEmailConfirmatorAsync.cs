using System;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public class EmailConfirmatorAsync : IEmailConfirmatorAsync
    {
        private IConfirmAsyncBehavior confirmAsyncBehavior;
        private IEmailService emailService;
        private IMessageBodyDictionary messageBody;

        public EmailConfirmatorAsync(IConfirmAsyncBehavior confirmAsyncBehavior,
                                     IEmailService emailService, 
                                     IMessageBodyDictionary messageBody)
        {
            this.confirmAsyncBehavior = confirmAsyncBehavior;
            this.emailService = emailService;
            this.messageBody = messageBody;
        }

        public async Task ConfirmEmailAsync(string Id, string token, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SendConfirmationEmail(string Id, string url)
        {
            throw new NotImplementedException();
        }
    }
}
