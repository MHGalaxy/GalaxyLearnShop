namespace GalaxyLearn.Core.DTOs.UserAdminPanel
{
    public class UserAdminPanelViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; } =string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public bool IsActive { get; set; }
        public string UserAvatar { get; set; } = string.Empty;
        public string RegisterDate { get; set; } = string.Empty;

    }
}
