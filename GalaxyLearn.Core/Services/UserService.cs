using GalaxyLearn.Core.DTOs.User;
using GalaxyLearn.Core.Generators;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.User;

namespace GalaxyLearn.Core.Services
{
    public class UserService : IUserService
    {
        private readonly GalaxyContext _context;

        public UserService(GalaxyContext context)
        {
            _context = context;
        }

        public bool PhoneNumberExist(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.UserEmail == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int AddTempCode(TempCode tempCode)
        {
            _context.TempCodes.Add(tempCode);
            _context.SaveChanges();
            return tempCode.TempCodeId;
        }

        public User? GetUserForLoginByPassword(LoginViewModel login)
        {
            string password = PasswordHelper.EncodePasswordSha256(login.Password);
            string phoneNumber = login.PhoneNumber.Trim();
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password);
        }

        public User? GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public bool ValidateRegisterTempCode(int userId, string code)
        {
            var expirationTime = DateTime.Now.AddSeconds(-120);

            return _context.TempCodes
                   .Any(tc =>
                       tc.UserId == userId &&
                       tc.Code == code &&
                       tc.Type == (byte)TempCodeType.Register &&
                       tc.SendCodeDateTime >= expirationTime);
        }

        public bool ActivateUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqueCode();
            _context.SaveChanges();

            return true;
        }

        public User? GetUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public bool ValidateForgetPasswordTempCode(int userId, string code)
        {
            var expirationTime = DateTime.Now.AddSeconds(-120);

            return _context.TempCodes
                   .Any(tc =>
                       tc.UserId == userId &&
                       tc.Code == code &&
                       tc.Type == (byte)TempCodeType.ForgetPassword &&
                       tc.SendCodeDateTime >= expirationTime);
        }

        public bool UpdateActiveCode(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return false;
            }

            user.ActiveCode = NameGenerator.GenerateUniqueCode();
            _context.SaveChanges();

            return true;
        }

        public string? GetActiveCode(int userId)
        {
            return _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.ActiveCode)
                .SingleOrDefault();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }
    }
}
