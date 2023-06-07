using AutoMapper;
using Cms.DTOs;
using Cms.Models;
using Cms.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
  //  [Route("v{version:apiVersion}/[controller]")]
 
     [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository cmsRepository ;
        private  IMapper mapper ;
        public CoursesController( ICmsRepository cmsRepository , IMapper mapper)
        {
            this.cmsRepository = cmsRepository;
            this.mapper = mapper;
        }

        // Approach1
        //   [HttpGet]
        //  public IEnumerable<Courses> GetCourses()
        //  {
        // return "Hello world";
        //    return cmsRepository.GetAllCourses();
        // }

        //Approach2
        //   Approach1  return type complex
        /*   [HttpGet]
           public IEnumerable<CourseDto> GetCourses()
           {
               try
               {
                   IEnumerable<Courses> courses = cmsRepository.GetAllCourses();
                   var result = MapCourseToCourseDto(courses);
                   return result;
               }
               catch(System.Exception)
               {
                   throw;
               }
           }
          */
        //Approach 2 return type IActionResult

        /*   [HttpGet]
           public IActionResult GetCourses()
           {
               try
               {
                   IEnumerable<Courses> courses = cmsRepository.GetAllCourses();
                   var result = MapCourseToCourseDto(courses);
                   return Ok(result);
               }
               catch (System.Exception ex)
               {
                   return StatusCode(StatusCodes.Status500InternalServerError, ex);
               }
           }
   */
        //Approach3 ActionResult<T>
           [HttpGet]
           public ActionResult<IEnumerable <CourseDto>> GetCourses()
           {
               try
               {
                   IEnumerable<Courses> courses = cmsRepository.GetAllCourses();
               
                //  var result = MapCourseToCourseDto(courses);
                var result = mapper.Map<CourseDto[]>(courses);
               // var result= Mapper.
                   return result.ToList(); //support ActionResult<T>
               }
               catch (System.Exception ex)
               {
                   return StatusCode(StatusCodes.Status500InternalServerError, ex);
               }
           }
        
        /*
        [HttpGet]
        public async Task <ActionResult<IEnumerable<CourseDto>>> GetCoursesAsync()
        {
            try
            {
                IEnumerable<Courses> courses = await cmsRepository.GetAllCoursesAsync();
                var result = MapCourseToCourseDto(courses);
                return result.ToList(); //support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
 */


        // custom mapper function
        private CourseDto MapCourseToCourseDto(Courses course)
        {
            return new CourseDto()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = (DTOs.COURSE_TYPE)course.CourseType
            };

        }


        private IEnumerable<CourseDto> MapCourseToCourseDto(IEnumerable<Courses> courses)
        {
            IEnumerable<CourseDto> result;
            result = courses.Select(c=> new CourseDto()
            {
                CourseID = c.CourseID,
                CourseName = c.CourseName,
                CourseDuration = c.CourseDuration,
                CourseType = (DTOs.COURSE_TYPE)c.CourseType
            }
          );

            return result;

        }


        [HttpPost]
        public ActionResult<CourseDto>AddCourse([FromBody] CourseDto courses)
        {
            try
            {

                var newCourse = mapper.Map<Courses>(courses);
                newCourse = cmsRepository.AddCourse(newCourse);
                return mapper.Map<CourseDto>(newCourse);
            }
            catch(System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourses(int courseId)
        {
            try
            {
                if (!cmsRepository.IsCourseExists(courseId))
                    return NotFound();
                Courses courses = cmsRepository.GetCourse(courseId);
                var result = mapper.Map<CourseDto>(courses);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut("{courseId}")]
        public ActionResult<CourseDto> UpdateCourse(int courseId, CourseDto course)
        {
            try
            {
                if (!cmsRepository.IsCourseExists(courseId))
                    return NotFound();
                Courses updatedCourse = mapper.Map<Courses>(course);
                updatedCourse = cmsRepository.UpdateCourse(courseId, updatedCourse);
                var result = mapper.Map<CourseDto>(updatedCourse);
                return result;

            }

            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{courseId}")]
        public ActionResult<CourseDto>DeleteCourses(int courseId)
        {
            try
            {
                if (!cmsRepository.IsCourseExists(courseId))
                    return NotFound();
                Courses courses = cmsRepository.DeleteCourse(courseId);
                if (courses == null)
                    return BadRequest();
                var result = mapper.Map<CourseDto>(courses);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        
// GET../courses/1/student
        [HttpGet("{courseId}/student")]
        public ActionResult<IEnumerable<StudentDto>> GetStudent(int courseId)
        {
            try
            {
                if (!cmsRepository.IsCourseExists(courseId))
                    return NotFound();
              IEnumerable <Student>student   = cmsRepository.GetStudents(courseId);
                var result = mapper.Map<StudentDto[]>(student);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost("{courseId}/student")]
        public ActionResult<StudentDto> AddStudent(int courseId , StudentDto student)
        {
            try
            {
                if (!cmsRepository.IsCourseExists(courseId))
                    return NotFound();
                Student newStudent = mapper.Map <Student>(student);

                //assign course
                Courses course = cmsRepository.GetCourse(courseId);
                newStudent.Course = course;
                newStudent = cmsRepository.AddStudent(newStudent);
                var result = mapper.Map<StudentDto>(newStudent);
                return StatusCode(StatusCodes.Status201Created, result);




                
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
