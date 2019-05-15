using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using WebApp.Models;
using WebApp.Models.HtmlNotifications;

namespace WebApp.AuthenticationPolicies
{
    public class ConfirmedEmailHandler : AuthorizationHandler<ConfirmedEmailRequirement>
    {
        private IAccountManager accountManager;
        private INotificationProvider notificationProvider;

        public ConfirmedEmailHandler(
            IAccountManager accountManager)
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
            else
            {
                var authContext = context.Resource as AuthorizationFilterContext;
                context.Succeed(requirement);

                authContext.Result = new RedirectToActionResult("MyProfile", "Profiles",new { });
            }

            return Task.CompletedTask;
        }
    }
}
