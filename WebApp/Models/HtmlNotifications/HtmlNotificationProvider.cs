using Microsoft.AspNetCore.Http;

namespace WebApp.Models.HtmlNotifications
{
    public interface INotificationProvider
    {
        void SetNotification(ISession session, string pClass,string content);
    }
    public class HtmlNotificationProvider : INotificationProvider
    {
        private INotificationBodyProvider bodyProvider;

        public HtmlNotificationProvider(INotificationBodyProvider bodyProvider)
        {
            this.bodyProvider = bodyProvider;
        }

        public void SetNotification(ISession session, string pClass, string content)
        {
            session.SetString("result",bodyProvider.GetNotificationBody(pClass,content));
        }
    }
}
