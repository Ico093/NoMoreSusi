using System;
using NoMoreSusi.Models;
using NoMoreSusi.Web.Mapping;
using NoMoreSusi.Web.ViewModels.Rooms;

namespace NoMoreSusi.Web.ViewModels.Lectures
{
	public class LectureViewModel:IMapFrom<Lecture>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DayOfWeek Day { get; set; }

		public int Hour { get; set; }

		public int Length { get; set; }

		public Course Course { get; set; }

		public int Teacher { get; set; }

		public RoomViewModel Room { get; set; }
	}
}