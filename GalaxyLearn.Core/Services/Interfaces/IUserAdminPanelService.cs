using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace GalaxyLearn.Core.Services.Interfaces
{
    public interface IUserAdminPanelService
    {
        List<UserAdminPanelViewModel> GetAllUsers();
        User GetUserByUserId(int userId);
        int AddUser(User user);
        int AddUserFromAdmin(CreateUserAdminPanelViewModel model);
        EditUserAdminPanelViewModel GetUserForEdit(int userId);
        void EditUserForAdmin(EditUserAdminPanelViewModel model);
        void DeleteUser(int userId);
    }
}
