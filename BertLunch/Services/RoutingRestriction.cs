using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BertLunch.Services
{
    public class RoutingRestriction : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Checking if the user has a role admin, if not 'access denied'
            var user = context.HttpContext.User;
            if (context.HttpContext.Request.Path.StartsWithSegments("/Admin") && !user.IsInRole("Admin"))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", new { area = "" });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
