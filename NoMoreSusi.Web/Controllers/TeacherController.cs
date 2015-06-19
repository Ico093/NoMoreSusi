using NoMoreSusi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMoreSusi.Web.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(INoMoreSusiData data)
            :base(data)
        {
            
        }

        // GET: Teacher
        public ActionResult Index()
        {
            return null;
        }
    }
}