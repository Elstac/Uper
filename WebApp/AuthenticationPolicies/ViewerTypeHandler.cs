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
            int id;
            
            if(!int.TryParse(authContext.RouteData.Values["id"].ToString(),out id))
            {
                if (!int.TryParse(authContext.RouteData.Values["tripId"].ToString(), out id))
                    throw new InvalidOperationException("Invalid tripId route value name");
            }

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
