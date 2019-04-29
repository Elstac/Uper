using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Data.Repositories;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public interface IStandardEmailConfirmatorFactory
    {
        IEmailConfirmator CreateConfirmator();
        IEmailConfirmationSender CreateCofirmationSender();
    }

    public class StandardEmailConfirmatorFactory:IStandardEmailConfirmatorFactory
    {
        private IServiceProvider serviceProvider;
        private IConfirmationProvider confirmationProvider;
        private IConfirmationTokenProvider confirmationTokenProvider;
        private IMessageBodyProvider messageBodyProvider;
        private string messageTemplate;

        /// <summary>
        /// Construct StandardEmailConfirmatorFactory.
        /// </summary>
        /// <param name="serviceProvider">Standard serivce provider from startup scope. Can by passed directly from DIC</param>
        /// <param name="confirmatorType">Type of confirmator, used in select implemetation of <code>IConfirmationProvider</code>,<code>IConfirmationTokenProvider</code> and <code>IMessageBodyProvider</code> setup in Startup.cs</param>
        /// <param name="messageTemplate">Message template name see templates.json file"/></param>
        public StandardEmailConfirmatorFactory(
            IServiceProvider serviceProvider,
            ConfirmatorType confirmatorType,
            string messageTemplate)
        {
            this.serviceProvider = serviceProvider;
            this.messageTemplate = messageTemplate;

            confirmationProvider = serviceProvider.GetService<Func<ConfirmatorType, IConfirmationProvider>>()
                .Invoke(confirmatorType);

            confirmationTokenProvider = serviceProvider.GetService<Func<ConfirmatorType, IConfirmationTokenProvider>>()
                .Invoke(confirmatorType);

            messageBodyProvider = serviceProvider.GetService<Func<ConfirmatorType, IMessageBodyProvider>>()
                .Invoke(confirmatorType);

        }

        public IEmailConfirmationSender CreateCofirmationSender()
        {
            return new EmailConfirmatorSender(
                serviceProvider.GetService<IEmailService>(),
                messageBodyProvider,
                serviceProvider.GetService<IApplicationUserRepository>(),
                confirmationTokenProvider,
                messageTemplate);
        }

        public IEmailConfirmator CreateConfirmator()
        {
            return new EmailConfirmator(confirmationProvider,
                                        serviceProvider.GetService<IApplicationUserRepository>());
        }
    }
}
