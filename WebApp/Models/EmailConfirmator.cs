using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.Models
{
    public interface IEmailConfirmator
    {
        Task SendConfirmationEmailAsync(ApplicationUser user, string url);
    }

    public class EmailConfirmator : IEmailConfirmator
    {
        private IEmailService emailService;
        private UserManager<ApplicationUser> userManager;

        public EmailConfirmator(IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            this.emailService = emailService;
            this.userManager = userManager;
        }

        public async Task SendConfirmationEmailAsync(ApplicationUser user,string url)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            emailService.SendMail("Uper", user.Email, "AccountConfirmation",
                new MessageBody().AddReplacement($"{user.UserName}", "{Name}")
                .AddReplacement($"<a href={url}?userId={user.Id}&token={token}>this link</a>", "{Link}"));
        }
    }
}
