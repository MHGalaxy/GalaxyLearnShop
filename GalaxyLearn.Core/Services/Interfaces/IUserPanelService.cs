using GalaxyLearn.Core.DTOs.UserPanel;
using GalaxyLearn.DataLayer.Entities.User;


namespace GalaxyLearn.Core.Services.Interfaces;

public interface IUserPanelService
{
    User? GetUserById(int userId);
    bool EditPorfile(int userId, UserEditProfileViewModel profile);
    bool DeleteImageProfile(int userId);
    bool ComparePassword(int userId, string password);
    bool UpdatePassword(int userId, string password);
}
