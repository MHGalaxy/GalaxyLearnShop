using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.Core.Extensions;
using GalaxyLearn.Core.Generators;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GalaxyLearn.Core.Services
{
    public class UserAdminPanelService : IUserAdminPanelService
    {
        private readonly GalaxyContext _context;

        public UserAdminPanelService(GalaxyContext context)
        {
            _context = context;
        }

        public List<UserAdminPanelViewModel> GetAllUsers()
        {
            var users = _context.Users
                .Select(u => new UserAdminPanelViewModel()
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    UserEmail = u.UserEmail,
                    RegisterDate = u.RegisterDate.ToPersianDateTime(),
                    IsActive = u.IsActive,
                    UserAvatar = u.UserAvatar
                }).ToList();

            return users;
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int AddUserFromAdmin(CreateUserAdminPanelViewModel model)
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserEmail = model.UserEmail,
                Password = PasswordHelper.EncodePasswordSha256(model.Password),
                UserAvatar = "user-avatar-default.png",
                RegisterDate = DateTime.Now,
                IsActive = model.IsActive,
                ActiveCode = NameGenerator.GenerateUniqueCode(),
            };

            //Save Image
            if (model.UserImage != null)
            {
                string imagePath = string.Empty;

                user.UserAvatar = "user-avatar-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.UserImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", user.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.UserImage.CopyTo(stream);
                }
            }

            return AddUser(user);
        }

        public EditUserAdminPanelViewModel GetUserForEdit(int userId)
        {
            return _context.Users
                .Include(u => u.UserRoles)
                .Where(u => u.UserId == userId)
                .Select(u => new EditUserAdminPanelViewModel()
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    UserEmail = u.UserEmail,
                    UserAvatar = u.UserAvatar,
                    IsActive = u.IsActive,
                    RoleIds = u.UserRoles.Select(u => u.RoleId).ToList()
                }).First();
        }


        public void EditUserForAdmin(EditUserAdminPanelViewModel model)
        {
            var user = GetUserByUserId(model.UserId);

            if (model.UserImage != null) //تصویر جدیدی اضافه شده
            {
                string imagePath = string.Empty;

                // حذف تصویر قبلی در صورت وجود
                if (model.UserAvatar != "user-avatar-default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", model.UserAvatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                model.UserAvatar = "user-avatar-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.UserImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user", model.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.UserImage.CopyTo(stream);
                }
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserEmail = model.UserEmail;
            user.UserAvatar = model.UserAvatar;
            user.IsActive = model.IsActive;
            if (!model.Password.IsNullOrEmpty())
            {
                user.Password = PasswordHelper.EncodePasswordSha256(model.Password);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserByUserId(userId);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
