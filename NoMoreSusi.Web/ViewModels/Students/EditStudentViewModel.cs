using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using NoMoreSusi.Common.Helpers;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Students
{
    public class EditStudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public EditStudentViewModel()
        {
            Courses = CoursesHelper.GetAll();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Student, EditStudentViewModel>()
                .ForMember(s => s.Course, s => s.MapFrom(st => st.Course.ToString()));

            configuration.CreateMap<EditStudentViewModel, Student>()
                .ForMember(s => s.Course, s => s.MapFrom(st => Enum.Parse(typeof(Course), st.Course)));
        }
    }
}