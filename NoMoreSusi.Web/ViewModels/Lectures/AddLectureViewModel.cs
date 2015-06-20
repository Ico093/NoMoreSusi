using System;
using NoMoreSusi.Web.Mapping;
using NoMoreSusi.Models;

namespace NoMoreSusi.Web.ViewModels.Lectures
{
	public class AddLectureViewModel:IMapFrom<Lecture>
	{
		public string Name { get; set; }

		public DayOfWeek Day { get; set; }

		public int Hour { get; set; }

		public int Length { get; set; }

		public Course Course { get; set; }

		public int TeacherId { get; set; }

		public int RoomId { get; set; }
	}
}