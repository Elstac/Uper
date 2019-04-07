using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public interface IEmailAddressValidator
    {
        bool ValidateEmailAddress(string emailAddress);
    }

    public class EmailAddressValidator : IEmailAddressValidator
    {
        public bool ValidateEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
