using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    interface IMessageBodyProvider
    {
        IMessageBodyDictionary GetBody();
    }
}
