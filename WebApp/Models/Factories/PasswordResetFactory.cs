using System;
using WebApp.Data.Repositories;
using WebApp.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models.EmailConfirmation
{
    public interface IPasswordResetFactory
    {
        IEmailConfirmationSender CreatePasswordResetSender(string name);
        IEmailConfirmator CreatePasswordResetConfirmator();
    }

    public class PasswordResetFactory : IPasswordResetFactory
    {
        private IServiceProvider serviceProvider;

        public PasswordResetFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEmailConfirmator CreatePasswordResetConfirmator()
        {
            return new EmailConfirmator(new PasswordResetConfirmationProvider(serviceProvider.GetService<UserManager<ApplicationUser>>()),
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }

        public IEmailConfirmationSender CreatePasswordResetSender(string name)
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
