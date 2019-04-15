using System.Collections.Generic;

namespace WebApp.Services
{
    public interface IMessageBodyDictionary
    {
        string GetReplacement(string key);
    }

    public class MessageBody : Dictionary<string, string>, IMessageBodyDictionary
    {
        public MessageBody AddReplacement(string value, string key)
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
