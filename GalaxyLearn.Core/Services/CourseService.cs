using GalaxyLearn.Core.Convertors;
using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Extensions;
using GalaxyLearn.Core.Generators;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.Common;
using GalaxyLearn.DataLayer.Entities.Course;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GalaxyLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly GalaxyContext _context;
        public CourseService(GalaxyContext context)
        {
            _context = context;
        }

        #region MenuGroup

        public List<MenuGroup> GetAllMenuGroups()
        {
            return _context.MenuGroups.ToList();
        }

        #endregion

        #region CourseGroup

        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }
        public CourseGroup GetCourseGroupById(int courseGroupId)
        {
            return _context.CourseGroups.Find(courseGroupId)!;
        }

        public List<CourseGroup> GetAllParentCourseGroups()
        {
            return _context.CourseGroups
                .Where(cg => cg.ParentId == null)
                .ToList();
        }

        public int AddCourseGroup(CourseGroup courseGroup)
        {
            _context.CourseGroups.Add(courseGroup);
            _context.SaveChanges();
            return courseGroup.CourseGroupId;
        }

        public void DeleteCourseGroup(int courseGroupId)
        {
            _context.CourseGroups.Remove(_context.CourseGroups.Find(courseGroupId));
            _context.SaveChanges();
        }

        public void EditCourseGroup(CourseGroup courseGroup)
        {
            _context.CourseGroups.Update(courseGroup);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
               .Select(g => new SelectListItem()
               {
                   Text = g.CourseGroupTitle,
                   Value = g.CourseGroupId.ToString()
               }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.CourseGroupTitle,
                    Value = g.CourseGroupId.ToString()
                }).ToList();
        }

        #endregion

        #region Course

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(r => r.RoleId == (int)Roles.Master).Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.FullName
                }).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(s => new SelectListItem()
            {
                Value = s.CourseLevelId.ToString(),
                Text = s.CourseLevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatues()
        {
            return _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.CourseStatusId.ToString(),
                Text = s.CourseStatusTitle
            }).ToList();
        }

        public int AddCourseFromAdmin(CreateCourseViewModel model)
        {
            var course = new Course()
            {
                CourseTitle = model.CourseTitle,
                CourseGroupId = model.CourseGroupId,
                CourseSubGroupId = model.CourseSubGroupId,
                TeacherId = model.TeacherId,
                CourseDescription = model.CourseDescription,
                CoursePrice = model.CoursePrice,
                IsLock = model.IsLock,
                CourseLevelId = model.CourseLevelId,
                CourseStatusId = model.CourseStatusId,
                Tags = model.Tags ?? string.Empty,
                CreateDate = DateTime.Now,
                CourseImageName = "course-image-default.png",
            };

            //Save Image
            if (model.CourseImageFile != null && model.CourseImageFile.IsImage())
            {
                course.CourseImageName = "course-image-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.CourseImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/images", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.CourseImageFile.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/thumb", course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            //Save Demo
            if (model.CourseDemoFile != null)
            {
                course.DemoFileName = "course-demo-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.CourseDemoFile.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/demo", course.DemoFileName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    model.CourseDemoFile.CopyTo(stream);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public List<CourseViewModel> GetAllCoursesForAdmin()
        {
            var courses = _context.Courses
                .Include(c => c.User)
                .Select(c => new CourseViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    CoursePrice = c.CoursePrice,
                    Teacher = c.User.FullName,
                    CourseImageName = c.CourseImageName,
                }).ToList();

            return courses;
        }

        public void DeleteCourse(int courseId)
        {
            _context.Courses.Remove(_context.Courses.Find(courseId));
            _context.SaveChanges();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public int EditCourseForAdmin(EditCourseViewModel model)
        {
            var course = GetCourseById(model.CourseId);

            //Save New Image
            if (model.CourseImageFile != null && model.CourseImageFile.IsImage())
            {
                //Delete Old Image
                if (course.CourseImageName != "course-image-default.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img//course/images", course.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img//course/thumb", course.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }

                course.CourseImageName = "course-image-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.CourseImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/images", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.CourseImageFile.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/thumb", course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            //Save Demo
            if (model.CourseDemoFile != null)
            {
                //Delete Old Demo
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.DemoFileName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                course.DemoFileName = "course-demo-" + NameGenerator.GenerateUniqueCode() + Path.GetExtension(model.CourseDemoFile.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/demo", course.DemoFileName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    model.CourseDemoFile.CopyTo(stream);
                }
            }

            course.CourseGroupId = model.CourseGroupId;
            course.CourseDescription = model.CourseDescription;
            course.CoursePrice = model.CoursePrice;
            course.CourseLevelId = model.CourseLevelId;
            course.CourseStatusId = model.CourseStatusId;
            course.CourseSubGroupId = model.CourseSubGroupId;
            course.CourseTitle = model.CourseTitle;
            course.TeacherId = model.TeacherId;
            course.IsLock = model.IsLock;
            course.Tags = model.Tags ?? string.Empty;

            _context.Courses.Update(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public bool DeleteCourseImage(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(x => x.CourseId == courseId);

            if (course.CourseImageName == "course-image-default.png")
            {
                return false;
            }

            string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/images", course.CourseImageName);
            if (File.Exists(deleteimagePath))
            {
                File.Delete(deleteimagePath);
            }

            string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/thumb", course.CourseImageName);
            if (File.Exists(deletethumbPath))
            {
                File.Delete(deletethumbPath);
            }

            course.CourseImageName = "course-image-default.png";
            _context.SaveChanges();

            return true;
        }

        public bool DeleteCourseDemo(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(x => x.CourseId == courseId);

            if (course.DemoFileName.IsNullOrEmpty())
            {
                return false;
            }

            //Delete Old Demo
            string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/course/demo", course.DemoFileName);
            if (File.Exists(deleteimagePath))
            {
                File.Delete(deleteimagePath);
            }

            course.DemoFileName = string.Empty;
            _context.SaveChanges();

            return true;
        }

        #endregion

        #region CourseLevel

        public List<CourseLevel> GetAllCourseLevels()
        {
            return _context.CourseLevels.ToList();
        }

        public CourseLevel GetCourseLevelById(int courseLevelId)
        {
            return _context.CourseLevels.Find(courseLevelId);
        }

        public int AddCourseLevel(CourseLevel courseLevel)
        {
            _context.CourseLevels.Add(courseLevel);
            _context.SaveChanges();
            return courseLevel.CourseLevelId;
        }

        public void EditCourseLevel(CourseLevel courseLevel)
        {
            _context.CourseLevels.Update(courseLevel);
            _context.SaveChanges();
        }

        public void DeleteCourseLevel(int courseLevelId)
        {
            var courseLevel = GetCourseLevelById(courseLevelId);
            _context.CourseLevels.Remove(courseLevel);
            _context.SaveChanges();
        }

        #endregion
    }
}
