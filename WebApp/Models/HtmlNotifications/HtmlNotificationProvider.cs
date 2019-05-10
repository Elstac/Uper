using Microsoft.AspNetCore.Http;

namespace WebApp.Models.HtmlNotifications
{
    public interface IHtmlNotificationProvider
    {
        void SetNotification(ISession session, string pClass,string content);
    }
    public class HtmlNotificationProvider : IHtmlNotificationProvider
    {
        private IHtmlNotificationBodyProvider bodyProvider;

        public HtmlNotificationProvider(IHtmlNotificationBodyProvider bodyProvider)
        {
            this.bodyProvider = bodyProvider;
        }

        public void SetNotification(ISession session, string pClass, string content)
        {
            session.SetString("result",bodyProvider.GetNotificationBody(pClass,content));
        }
    }
}
