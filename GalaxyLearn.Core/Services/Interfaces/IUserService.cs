using GalaxyLearn.Core.DTOs.User;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace GalaxyLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool PhoneNumberExist(string phoneNumber);
        bool EmailExists(string email);
        int AddUser(User user);
        int AddTempCode(TempCode tempCode);
        User? GetUserForLoginByPassword(LoginViewModel login);
        User? GetUserByActiveCode(string activeCode);
        bool ValidateRegisterTempCode(int userId, string code);
        bool ActivateUser(int userId);
        User? GetUserByPhoneNumber(string phoneNumber);
        bool ValidateForgetPasswordTempCode(int userId, string code);
        bool UpdateActiveCode(int userId);
        string? GetActiveCode(int userId);
        bool UpdateUser(User user);
    }
}
