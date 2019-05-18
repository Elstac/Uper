using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Models;

namespace WebApp.AuthenticationPolicies
{
    public class ViewerTypeHandler : AuthorizationHandler<ViewerTypeRequirement>
    {
        private IViewerTypeProvider viewerTypeProvider;

        public ViewerTypeHandler(IViewerTypeProvider viewerTypeProvider)
        {
            this.viewerTypeProvider = viewerTypeProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewerTypeRequirement requirement)
        {
            var authContext = context.Resource as AuthorizationFilterContext;
            string tmp;
            
            if(string.IsNullOrEmpty(authContext.HttpContext.Request.Query["id"].ToString()))
            {
                if (string.IsNullOrEmpty(authContext.HttpContext.Request.Query["tripId"].ToString()))
                    throw new InvalidOperationException("Invalid tripId route value name");
                else
                    tmp = "tripId";
            }
            else
            {
                tmp = "id";
            }

            var id = int.Parse(authContext.HttpContext.Request.Query[tmp].ToString());

            var viewerType = await viewerTypeProvider.GetViewerTypeAsync(context.User, id);

            foreach (var type in requirement.Types)
            {
                if(viewerType== type)
                {
                    context.Succeed(requirement);
                    break;
                }
            }
        }
    }
}
