using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMoreSusi.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            ErrorMessage();

            return View();
        }


        [Conditional("DEBUG")]
        private void ErrorMessage()
        {
        }
    }
}