using System;
using WebApp.Data.Repositories;
using WebApp.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models.EmailConfirmation
{
    public interface IPasswordResetConfirmatorFactory
    {
        IEmailConfirmationSender CreateAccountConfirmatorSender(string name);
        IEmailConfirmator CreateAccountConfirmator();
    }

    public class PasswordResetConfirmatorFactory : IPasswordResetConfirmatorFactory
    {
        private IServiceProvider serviceProvider;

        public PasswordResetConfirmatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEmailConfirmator CreateAccountConfirmator()
        {
            return new EmailConfirmator(new PasswordResetConfirmationProvider(serviceProvider.GetService<UserManager<ApplicationUser>>()),
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }

        public IEmailConfirmationSender CreateAccountConfirmatorSender(string name)
        {
            var body = new MessageBodyDictionary();
            body.AddReplacement(name, "{Name}")
                .AddReplacement("to reset your password", "{Purpose}");

            return new EmailConfirmatorSender(serviceProvider.GetService<IEmailService>(),
                body,
                serviceProvider.GetService<IApplicationUserRepository>(),
                new PasswordResetTokenProvider(serviceProvider.GetService<UserManager<ApplicationUser>>()),
                "HyperlinkConfirmation");
        }
    }
}
