using GalaxyLearn.Core.DTOs.UserPanel;
using GalaxyLearn.Core.Generators;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GalaxyLearn.Core.Services
{
    public class UserPanelService : IUserPanelService
    {
        private readonly GalaxyContext _context;

        public UserPanelService(GalaxyContext context)
        {
            _context = context;
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public bool EditPorfile(int userId, UserEditProfileViewModel profile)
        {
            if (profile.UserImage != null) //تصویر جدیدی اضافه شده
            {
                string imagePath = string.Empty;

                // حذف تصویر قبلی در صورت وجود
                if (profile.UserAvatar != "user-avatar-default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", profile.UserAvatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.UserAvatar = "user-avatar-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(profile.UserImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", profile.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserImage.CopyTo(stream);
                }
            }

            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.UserEmail = profile.UserEmail;
            user.UserAvatar = profile.UserAvatar;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteImageProfile(int userId)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", user.UserAvatar);

            if (user.UserAvatar == "user-avatar-default.png")
            {
                return false;
            }

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            user.UserAvatar = "user-avatar-default.png";
            _context.SaveChanges();

            return true;
        }

        public bool ComparePassword(int userId, string password)
        {
            var hashedPassword = PasswordHelper.EncodePasswordSha256(password);
            return _context.Users.Any(u => u.UserId == userId && u.Password == hashedPassword);
        }

        public bool UpdatePassword(int userId, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }

            user.Password = PasswordHelper.EncodePasswordSha256(password);
            _context.SaveChanges();

            return true;
        }
    }
}
