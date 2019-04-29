using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    /// <summary>
    /// Provides custom message body for EmailConfirmatorFactories
    /// </summary>
    public interface IMessageBodyProvider
    {
        IMessageBodyDictionary GetBody(params object[] par);
    }
}
