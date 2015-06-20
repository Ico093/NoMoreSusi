using System.Web.Mvc;
using NoMoreSusi.Web.Infrastrucutre.Filters;

namespace NoMoreSusi.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			filters.Add(new CustomErrorAttribute());
        }
    }
}
