using AutoMapper;
using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.Core.DTOs.CourseGroup;
using GalaxyLearn.Core.DTOs.CourseLevel;
using GalaxyLearn.Core.DTOs.Role;
using GalaxyLearn.Core.DTOs.UserPanel;
using GalaxyLearn.Core.Extensions;
using GalaxyLearn.DataLayer.Entities.Course;
using GalaxyLearn.DataLayer.Entities.User;
using GalaxyLearn.Web.Pages.Admin.CourseGroups;

namespace GalaxyLearn.Web.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            #region User

            CreateMap<User, UserEditProfileViewModel>();
            CreateMap<UserEditProfileViewModel, User>()
                .ForMember(dst => dst.PhoneNumber, src => src.Ignore());

            CreateMap<User, UserDashboardViewModel>()
                .ForMember(dst => dst.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate.ToPersianDate()));
            #endregion

            #region Role

            CreateMap<Role, RoleViewModel>();
            CreateMap<CreateRoleViewModel,Role>();
            CreateMap<EditRoleViewModel,Role>();
            CreateMap<Role, EditRoleViewModel>();

            #endregion

            #region CourseGroup

            CreateMap<CreateCourseGroupViewModel, CourseGroup>();
            CreateMap<EditCourseGroupViewModel, CourseGroup>();
            CreateMap<CourseGroup, EditCourseGroupViewModel>();
            CreateMap<CourseGroup, CourseGroupViewModel>();

            #endregion

            #region Course

            CreateMap<Course, EditCourseViewModel>();

            #endregion

            #region CourseLevel

            CreateMap<CourseLevel, CourseLevelViewModel>();
            CreateMap<CreateCourseLevelViewModel, CourseLevel>();
            CreateMap<EditCourseLevelViewModel, CourseLevel>();
            CreateMap<CourseLevel, EditCourseLevelViewModel>();

            #endregion
        }
    }
}
