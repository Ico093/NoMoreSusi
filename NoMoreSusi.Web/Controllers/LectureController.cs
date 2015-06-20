using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;

namespace NoMoreSusi.Web.Controllers
{
    public class LectureController : BaseController
    {
        public LectureController(INoMoreSusiData data)
            :base(data)
        {
            
        }
        // GET: Lecture
        public ActionResult Index()
        {
            return null;
        }
    }
}