using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GalaxyLearn.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;   
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Get Service from builder Services
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;

            if (!context.HttpContext.User.Identity!.IsAuthenticated) // Check user Authenticated
            {
                context.Result = new RedirectResult("/Login");
            }
            else
            {
                // Check user has Permission
                var userId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                if (!_permissionService.CheckPermission(_permissionId, userId))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
        }
    }
}
