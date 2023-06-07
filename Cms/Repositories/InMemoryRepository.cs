using Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Repositories
{
    public class InMemoryRepository : ICmsRepository
    {
        List<Courses> courses = null;
        List<Student> student = null;
        public InMemoryRepository()
        {

            courses = new List<Courses>();
            student = new List<Student>();
            courses.Add(
                new Courses()
                {
                    CourseID = 1,
                    CourseName= "Computer Science" ,
                    CourseDuration =4,
                    CourseType= COURSE_TYPE.ENGINEERING
                }
                );

            courses.Add(
                new Courses()
                {
                    CourseID = 2,
                    CourseName = "Information Technology",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.ENGINEERING
                }
                );
            student.Add(
                new Student()
                {
                    StudentId = 101,
                    FirstName = "arya",
                    LastName = "jha",
                    PhoneNumber = "89765432",
                    Address = "up",
                    Course = courses.Where(c => c.CourseID == 1).SingleOrDefault()

                }

                ) ; 
            
        }
        public IEnumerable<Courses> GetAllCourses()
        {
            return courses;
        }

        public async Task<IEnumerable<Courses>> GetAllCoursesAsync()
        {
            return await Task.Run(()=> courses.ToList());
        }

        public Courses AddCourse(Courses newCourse)
        {
            var maxCourseId = courses.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseId + 1;
            courses.Add(newCourse);
            return newCourse;
        }

        public bool IsCourseExists(int courseId)
        {
            return courses.Any(c => c.CourseID == courseId);
        }

        public Courses GetCourse(int courseId)
        {
            var result = courses.Where(c => c.CourseID == courseId).SingleOrDefault();
            return result;
        }

        public Courses UpdateCourse(int courseId, Courses updatedCourse)
        {
            var course = courses.Where(c => c.CourseID == courseId).SingleOrDefault();
            if(course !=null)
            {
                course.CourseName = updatedCourse.CourseName;
                course.CourseDuration = updatedCourse.CourseDuration;
                course.CourseType = updatedCourse.CourseType;
            }
            return course;
        }

        public Courses DeleteCourse(int courseId)
        {
            var course = courses.Where(c => c.CourseID == courseId).SingleOrDefault();
            if(course!=null)
            {
                courses.Remove(course);
            }
            return course;
        }

       public IEnumerable<Student> GetStudents(int courseId)
        {
            return student.Where(s => s.Course.CourseID == courseId);
        }

        public Student AddStudent(Student newStudent)
        {
            var maxStudentId = student.Max(c => c.StudentId);
            newStudent.StudentId = maxStudentId + 1;
           student.Add(newStudent);
            return newStudent;
        }
    }
}
