using System;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Students
{
    public class StudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentViewModel>()
                .ForMember(s => s.Course, s => s.MapFrom(st => st.Course.ToString()));

            configuration.CreateMap<StudentViewModel, Student>()
                .ForMember(s => s.Course, s => s.MapFrom(st => Enum.Parse(typeof(Course), st.Course)));
        }
    }
}