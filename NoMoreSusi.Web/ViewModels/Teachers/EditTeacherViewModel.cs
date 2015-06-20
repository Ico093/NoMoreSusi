using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using NoMoreSusi.Common.Helpers;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;

namespace NoMoreSusi.Web.ViewModels.Teachers
{
    public class EditTeacherViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
        }
    }
}