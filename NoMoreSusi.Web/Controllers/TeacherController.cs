using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;

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