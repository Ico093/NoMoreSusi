using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NoMoreSusi.Models;

namespace NoMoreSusi.Common.Helpers
{
	public static class EnumToListHelper
	{
		public static IEnumerable<DayOfWeek> TransformDaysOfWeek()
		{
			IEnumerable<DayOfWeek> values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

			return values;
		}
	}
}
