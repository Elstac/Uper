using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.AuthenticationPolicies
{
    public class ViewerTypeRequirement: IAuthorizationRequirement
    {
        public IEnumerable<ViewerType> Types { get; set; }

        public ViewerTypeRequirement(IEnumerable<ViewerType> types)
        {
            Types = types;
        }

    }
}
