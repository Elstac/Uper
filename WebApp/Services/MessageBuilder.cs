using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IMessageBuilder
    {
        void SetTemplate(string template);
        void SetHead(string text);
        void SetFooter(string text);
        void SetBody(string text);
    }

    public class MessageBuilder : IMessageBuilder
    {
        public void SetBody(string text)
        {
            throw new NotImplementedException();
        }

        public void SetFooter(string text)
        {
            throw new NotImplementedException();
        }

        public void SetHead(string text)
        {
            throw new NotImplementedException();
        }

        public void SetTemplate(string template)
        {
            throw new NotImplementedException();
        }
    }
}
