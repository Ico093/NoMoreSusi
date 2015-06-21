using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoMoreSusi.Models
{
	public class Shedule
	{
		public int Id { get; set; }

		public DayOfWeek Day { get; set; }

		public int Hour { get; set; }

		public int LectureId { get; set; }

		public int RoomId { get; set; }


		[ForeignKey("LectureId")]
		public Lecture Lecture { get; set; }

		[ForeignKey("RoomId")]
		public Room Room { get; set; }
	}
}
