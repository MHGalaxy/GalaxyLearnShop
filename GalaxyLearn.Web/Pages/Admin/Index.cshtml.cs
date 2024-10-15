using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin
{
    [PermissionChecker((int)PermissionEnum.AdminPanel)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
