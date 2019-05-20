using System;
using System.Collections.Generic;
using WebApp.Models.EmailConfirmation;
using WebApp.Services;

namespace WebApp.Models.TravellChangeEmail
{
    public class OfferStateMessageProvider : IMessageBodyProvider
    {
        private Dictionary<OfferStateChange,(string, string)> dict;

        public OfferStateMessageProvider()
        {
            dict = new Dictionary<OfferStateChange,(string,string) >();
            dict.Add(OfferStateChange.RequestAccepted, ("Pending", "Accepted"));
            dict.Add(OfferStateChange.UserRemoved, ("Accepted", "Removed"));
            dict.Add(OfferStateChange.Deleted, ("Pending", "Deleted"));
        }

        public IMessageBodyDictionary GetBody(params object[] par)
        {
            var tmp = dict[(OfferStateChange)par[2]];
            var ret = new MessageBodyDictionary()
                .AddReplacement(par[0].ToString(), "{Name}")
                .AddReplacement(par[1].ToString(), "{Link}")
                .AddReplacement(tmp.Item1,"{OldState}")
                .AddReplacement(tmp.Item2,"{NewState}");

            return ret;
        }
    }
}
