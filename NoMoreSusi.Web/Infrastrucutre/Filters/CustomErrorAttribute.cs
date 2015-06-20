using System;
using System.Web.Mvc;

namespace NoMoreSusi.Web.Infrastrucutre.Filters
{
	public class CustomErrorAttribute:HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
		}
	}
}