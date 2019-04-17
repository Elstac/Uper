using System.Collections.Generic;

namespace WebApp.Services
{
    public interface IMessageBodyDictionary
    {
        string GetReplacement(string key);
    }

    public class MessageBodyDictionary : Dictionary<string, string>, IMessageBodyDictionary
    {
        public MessageBodyDictionary AddReplacement(string value, string key)
        {
            Add(key, value);
            return this;
        }

        public string GetReplacement(string key)
        {
            return this[key];
        }
    }
}
