using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;

namespace NoMoreSusi.Web.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(INoMoreSusiData data)
            :base(data)
        {
            
        }
        // GET: Room
        public ActionResult Index()
        {
            return null;
        }
    }
}