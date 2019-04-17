using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Services;

namespace WebApp.Models
{
    public interface IPasswordResetProvider
    {
        void SendPasswordResetEmail(string userId, string url);
        Task ResetUserPasswordAsync(string userId, string token, string newPassword);
    }

    public class PasswordResetProvider : IPasswordResetProvider
    {
        private IApplicationUserRepository repository;
        private IEmailService emailService;
        private UserManager<ApplicationUser> userManager;

        public Task ResetUserPasswordAsync(string userId, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void SendPasswordResetEmail(string userId, string url)
        {
            throw new NotImplementedException();
        }
    }
}
