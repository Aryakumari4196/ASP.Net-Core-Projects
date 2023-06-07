using AutoMapper;
using Cms.DTOs;
using Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Mapper
{
    public class CmsMapper : Profile
    {
        public CmsMapper()
        {
            CreateMap<CourseDto, Courses>()
                .ReverseMap();
           CreateMap<StudentDto, Student>()
                .ReverseMap();
        }
    }
}
