using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Services;

namespace WebApp.Models
{
    public interface IEmailConfirmator
    {
        Task SendConfirmationEmailAsync(ApplicationUser user, string url);
        Task ConfirmAcconuntAsync(string userId, string token);
    }

    public class EmailConfirmator : IEmailConfirmator
    {
        private IApplicationUserRepository userRepository;
        private IEmailService emailService;
        private UserManager<ApplicationUser> userManager;

        public EmailConfirmator(IEmailService emailService, UserManager<ApplicationUser> userManager,IApplicationUserRepository userRepository)
        {
            this.emailService = emailService;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task ConfirmAcconuntAsync(string userId, string token)
        {
            var user = userRepository.GetById(userId);
            var response = await userManager.ConfirmEmailAsync(user, token);

            if (!response.Succeeded)
                throw new InvalidOperationException("Account confirmation failed");
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
