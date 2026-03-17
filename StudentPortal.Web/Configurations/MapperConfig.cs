using AutoMapper;
using StudentPortal.Web.Dtos;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<EditStudentViewModel, Student>().ReverseMap();
        }
    }
}
