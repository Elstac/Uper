using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ProfileEditViewModel
    {
        public ProfileEditViewModel(string Ans)
        {
            this.Ans = Ans;
            this.PasswordErrors = null;
        }

        public ProfileEditViewModel(string Ans, IEnumerable<IdentityError> PasswordErrors)
        {
            this.Ans = Ans;
            this.PasswordErrors = PasswordErrors;
        }

        public string Ans { get; set; }

        public IEnumerable<IdentityError> PasswordErrors { get; set; }
    }
}
