using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Cms.Models.Courses;

namespace Cms.Models
{
    public class Courses
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public int CourseDuration { get; set; }
      
        public COURSE_TYPE CourseType { get; set; }

    }

    public enum COURSE_TYPE
    {
        ENGINEERING,
        MEDICAL ,
        MANAGEMENT
    }
}
