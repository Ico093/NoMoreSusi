using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NoMoreSusi.Models;

namespace NoMoreSusi.Common.Helpers
{
    public static class CoursesHelper
    {
        public static List<SelectListItem> GetAll()
        {
            List<SelectListItem> courses = (from object cource in Enum.GetValues(typeof(Course))
                                            select new SelectListItem
                                            {
                                                Text = cource.ToString(),
                                                Value = ((int)cource).ToString()
                                            }).ToList();

            return courses;
        }
    }
}
