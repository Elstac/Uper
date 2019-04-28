using System;

namespace WebApp.Models.EmailConfirmation
{
    public interface IAccountEmailConfirmatorFactory:IStandardEmailConfirmatorFactory
    {
    }

    class AccountEmailConfirmatorFactory : StandardEmailConfirmatorFactory, IAccountEmailConfirmatorFactory
    {
        public AccountEmailConfirmatorFactory(IServiceProvider serviceProvider)
            : base(serviceProvider,
                  ConfirmatorType.Account, 
                  "HyperlinkConfirmation")
        {
        }
    }
}
