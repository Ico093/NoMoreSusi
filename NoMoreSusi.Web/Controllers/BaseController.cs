using System.Web.Mvc;
using NoMoreSusi.Data.Interfaces;

namespace NoMoreSusi.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly INoMoreSusiData _data;

        public BaseController(INoMoreSusiData data)
        {
            _data = data;
        }

        protected INoMoreSusiData Data
        {
            get
            {
                return _data;
            }
        }
    }
}