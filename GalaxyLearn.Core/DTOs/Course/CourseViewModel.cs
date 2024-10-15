using GalaxyLearn.DataLayer.Entities.Course;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.Course
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }

        public string Teacher { get; set; }

        public int CoursePrice { get; set; }

        public string CourseImageName { get; set; }
    }
}
