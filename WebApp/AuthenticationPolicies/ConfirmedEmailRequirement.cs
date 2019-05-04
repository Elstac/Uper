using Microsoft.AspNetCore.Authorization;

namespace WebApp.AuthenticationPolicies
{
    public class ConfirmedEmailRequirement: IAuthorizationRequirement
    {
    }
}
