using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Data.Repositories;
using WebApp.Services;

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
        private IMessageBodyProvider messageBodyProvider;

        public PasswordResetFactory(IServiceProvider serviceProvider)
        {
            messageBodyProvider = new PasswordResetMessageProvider();
            this.serviceProvider = serviceProvider;
        }

        public IEmailConfirmator CreatePasswordResetConfirmator()
        {
            return new EmailConfirmator(serviceProvider.GetService<PasswordResetConfirmationProvider>(),
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }

        public IEmailConfirmationSender CreatePasswordResetSender(string name)
        {
            return new EmailConfirmatorSender(serviceProvider.GetService<IEmailService>(),
                messageBodyProvider.GetBody(),
                serviceProvider.GetService<IApplicationUserRepository>(),
                serviceProvider.GetService<PasswordResetTokenProvider>(),
                "HyperlinkConfirmation");
        }
    }
}
