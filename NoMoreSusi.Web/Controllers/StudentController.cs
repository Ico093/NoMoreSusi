using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;

namespace NoMoreSusi.Web.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(INoMoreSusiData data)
            :base(data)
        {
            
        }

        // GET: Student
        public ActionResult Index()
        {
            return null;
        }
    }
}