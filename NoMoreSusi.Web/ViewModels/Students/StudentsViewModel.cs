using System;
using System.Data.Entity.SqlServer;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Students
{
    public class StudentsViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Email { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentsViewModel>()
                .ForMember(s => s.Course, s => s.MapFrom(st => st.Course.ToString()))
                .ForMember(s => s.Email, s => s.MapFrom(st => st.User.Email));

            configuration.CreateMap<StudentsViewModel, Student>()
                .ForMember(s => s.Course, s => s.MapFrom(st => Enum.Parse(typeof(Course), st.Course)));
        }
    }
}