using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    /// <summary>
    /// Provides custom message body for EmailConfirmatorFactories
    /// </summary>
    interface IMessageBodyProvider
    {
        IMessageBodyDictionary GetBody();
    }
}
