using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.DataLayer.Entities.Common;
using GalaxyLearn.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalaxyLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region MenuGroup

        List<MenuGroup> GetAllMenuGroups();

        #endregion

        #region CourseGroup

        List<CourseGroup> GetAllCourseGroups();
        CourseGroup GetCourseGroupById(int courseGroupId);
        List<CourseGroup> GetAllParentCourseGroups();
        int AddCourseGroup(CourseGroup courseGroup);
        void EditCourseGroup(CourseGroup courseGroup);
        void DeleteCourseGroup(int courseGroupId);
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);

        #endregion

        #region Course
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatues();
        int AddCourseFromAdmin(CreateCourseViewModel model);
        List<CourseViewModel> GetAllCoursesForAdmin();
        void DeleteCourse(int courseId);
        Course GetCourseById(int courseId);
        int EditCourseForAdmin(EditCourseViewModel model);
        bool DeleteCourseImage(int courseId);
        bool DeleteCourseDemo(int courseId);
        #endregion

        #region CourseLevel
        List<CourseLevel> GetAllCourseLevels();
        CourseLevel GetCourseLevelById(int courseLevelId);
        int AddCourseLevel(CourseLevel courseLevel);
        void EditCourseLevel(CourseLevel courseLevel);
        void DeleteCourseLevel(int courseLevelId);
        #endregion
    }
}
