using System.ComponentModel.DataAnnotations.Schema;

namespace NoMoreSusi.Models
{
	public class Unsheduled : Shedule
	{
		public int Id { get; set; }

		public int LectureId { get; set; }

		[ForeignKey("LectureId")]
		public Lecture Lecture { get; set; }
	}
}
