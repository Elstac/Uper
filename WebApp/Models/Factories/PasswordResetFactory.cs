using System;

namespace WebApp.Models.EmailConfirmation
{
    public interface IPasswordResetFactory :IStandardEmailConfirmatorFactory
    {
    }

    class PasswordResetFactory : StandardEmailConfirmatorFactory, IPasswordResetFactory
    {
        public PasswordResetFactory(IServiceProvider serviceProvider) 
            : base(
                  serviceProvider,
                  ConfirmatorType.PasswordReset,
                  "HyperlinkConfirmation")
        {

        }
    }
}
