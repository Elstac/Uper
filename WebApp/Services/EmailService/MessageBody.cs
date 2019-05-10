using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.Services
{
    public interface IMessageBodyDictionary
    {
        string GetReplacement(string key);
        IMessageBodyDictionary AddReplacement(string value, string key);
    }
    [ExcludeFromCodeCoverage]
    public class MessageBodyDictionary : Dictionary<string, string>, IMessageBodyDictionary
    {
        public IMessageBodyDictionary AddReplacement(string value, string key)
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
