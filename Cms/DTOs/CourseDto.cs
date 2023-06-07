using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cms.DTOs
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        [Required]
        [MaxLength (50)]
        public string CourseName { get; set; }
        [Required]
        [Range(1,5)]
        public int CourseDuration { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public COURSE_TYPE CourseType { get; set; }
    }

    public enum COURSE_TYPE
    {
        ENGINEERING,
        MEDICAL,
        MANAGEMENT
    }


}
