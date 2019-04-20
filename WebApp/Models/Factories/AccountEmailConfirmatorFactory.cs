using System;
using WebApp.Data.Repositories;
using WebApp.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using Microsoft.AspNetCore.Identity;

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

        public AccountEmailConfirmatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEmailConfirmator CreateAccountConfirmator()
        {
            return new EmailConfirmator(new AccountConfirmationProvider(serviceProvider.GetService<UserManager<ApplicationUser>>()), 
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }

        public IEmailConfirmationSender CreateAccountConfirmatorSender(string name)
        {
            var body = new MessageBodyDictionary();
            body.AddReplacement(name, "{Name}")
                .AddReplacement("to confirm your account", "{Purpose}");

            return new EmailConfirmatorSender(serviceProvider.GetService<IEmailService>(),
                body,
                serviceProvider.GetService<IApplicationUserRepository>(),
                new AccountTokenProvider(serviceProvider.GetService<UserManager<ApplicationUser>>()),
                "HyperlinkConfirmation");
        }
    }
}
