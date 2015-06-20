using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Web.ViewModels.Lectures;

namespace NoMoreSusi.Web.Controllers
{
    public class LectureController : BaseController
    {
        public LectureController(INoMoreSusiData data)
            :base(data)
        {
            
        }

        public ActionResult Index()
        {
            return null;
        }

		public ActionResult Add(int id)
		{
			var viewmodel = new AddLectureViewModel()
			{
				TeacherId = id
			};

			return View(viewmodel);
		}
    }
}