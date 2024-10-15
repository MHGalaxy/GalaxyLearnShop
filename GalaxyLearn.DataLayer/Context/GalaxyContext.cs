using GalaxyLearn.DataLayer.Entities.Common;
using GalaxyLearn.DataLayer.Entities.Course;
using GalaxyLearn.DataLayer.Entities.Permission;
using GalaxyLearn.DataLayer.Entities.User;
using GalaxyLearn.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace GalaxyLearn.DataLayer.Context
{
    public class GalaxyContext : DbContext
    {
        public GalaxyContext(DbContextOptions<GalaxyContext> options) : base(options)
        {

        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TempCode> TempCodes { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Common

        public DbSet<MenuGroup> MenuGroups { get; set; }

        #endregion

        #region Course

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
