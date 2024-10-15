using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {
            
        }

        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion
    }
}
