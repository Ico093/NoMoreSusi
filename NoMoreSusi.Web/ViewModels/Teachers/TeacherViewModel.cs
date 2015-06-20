using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Teachers
{
    public class TeacherViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
        }
    }
}