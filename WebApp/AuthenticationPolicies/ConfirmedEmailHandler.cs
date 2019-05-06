using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.AuthenticationPolicies
{
    public class ConfirmedEmailHandler : AuthorizationHandler<ConfirmedEmailRequirement>
    {
        private IAccountManager accountManager;
        public ConfirmedEmailHandler(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfirmedEmailRequirement requirement)
        {
            var task = accountManager.GetUserAsync(context.User);
            var user = task.Result;
            if (user.EmailConfirmed)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
