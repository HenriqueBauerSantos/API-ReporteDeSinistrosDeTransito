using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Api_InfoTransito.Extensions;

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}

public class CustomAuthorization
{
    public static bool ValidateClaimsUser(HttpContext context, string cliamName, string claimValue)
    {
        if(context.User.Identity == null)
            return false;

        return context.User.Identity.IsAuthenticated &&
            context.User.Claims.Any(c => c.Type == cliamName && c.Value.Split(',').Contains(claimValue));
    }
}

public class RequirementClaimFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequirementClaimFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidateClaimsUser(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result= new StatusCodeResult(403);
        }
    }
}
