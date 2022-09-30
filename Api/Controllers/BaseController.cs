using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingApp.Api.Controllers;

public class BaseController : ControllerBase
{
    protected string UserId => FindClaim(ClaimTypes.NameIdentifier);

    private string FindClaim(string claimName)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

        var claim = claimsIdentity.FindFirst(claimName);

        return claim?.Value;
    }
}