using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Data.Repositories;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public interface IAccountEmailConfirmatorFactory
    {
        IEmailConfirmationSender CreateAccountConfirmatorSender(string name);
        IEmailConfirmator CreateAccountConfirmator();
    }

    public class AccountEmailConfirmatorFactory:IAccountEmailConfirmatorFactory
    {
        private IServiceProvider serviceProvider;
        private IMessageBodyProvider messageBodyProvider;

        public AccountEmailConfirmatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            messageBodyProvider = new AccountMessageProvider();
        }

        public IEmailConfirmator CreateAccountConfirmator()
        {
            return new EmailConfirmator(serviceProvider.GetService<AccountConfirmationProvider>(),
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }

        public IEmailConfirmationSender CreateAccountConfirmatorSender(string name)
        {
            return new EmailConfirmatorSender(serviceProvider.GetService<IEmailService>(),
                messageBodyProvider.GetBody(),
                serviceProvider.GetService<IApplicationUserRepository>(),
                serviceProvider.GetService<AccountTokenProvider>(),
                "HyperlinkConfirmation");
        }
    }
}
