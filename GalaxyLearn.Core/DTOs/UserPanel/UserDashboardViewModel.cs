using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.UserPanel
{
    public class UserDashboardViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string RegisterDate { get; set; } = string.Empty;
        public string UserAvatar { get; set; } = "user-avatar-default.png";
        public int UserAcountBalance { get; set; }
    }
}
