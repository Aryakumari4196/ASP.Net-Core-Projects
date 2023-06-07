using Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Repositories
{
   public interface ICmsRepository
    {
        IEnumerable<Courses> GetAllCourses();
       Task<IEnumerable<Courses>> GetAllCoursesAsync();
        Courses AddCourse(Courses newCourse);
        bool IsCourseExists(int courseId);
        Courses GetCourse(int courseId);
        Courses UpdateCourse(int courseId, Courses newCourse);
        Courses DeleteCourse(int courseId);
        IEnumerable<Student> GetStudents(int courseId);
        Student AddStudent( Student student);


    }
}
