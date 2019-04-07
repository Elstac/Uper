using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Exceptions;
namespace WebApp.Models
{
    public interface IEmailAddressValidator
    {
        bool ValidateEmailAddress(string emailAddress);
    }

    public class EmailAddressValidator : IEmailAddressValidator
    {
        private string pattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

        public bool ValidateEmailAddress(string emailAddress)
        {
            if (!Regex.IsMatch(emailAddress, pattern))
                throw new InvalidEmailAddressException("Invalid email address");

            return true;
        }
    }
}
