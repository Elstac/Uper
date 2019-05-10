using Microsoft.AspNetCore.Http;

namespace WebApp.Models.HtmlNotifications
{
    public interface IHtmlNotificationProvider
    {
        void SetNotification(HttpContext context,string pClass,string content);
    }
    public class HtmlNotificationProvider : IHtmlNotificationProvider
    {
        private IHtmlNotificationBodyProvider bodyProvider;

        public HtmlNotificationProvider(IHtmlNotificationBodyProvider bodyProvider)
        {
            this.bodyProvider = bodyProvider;
        }

        public void SetNotification(HttpContext context, string pClass, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}
