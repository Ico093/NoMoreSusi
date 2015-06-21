using System.ComponentModel.DataAnnotations.Schema;

namespace NoMoreSusi.Models
{
	public class Unsheduled
	{
		public int Id { get; set; }

		public int LectureId { get; set; }

		[ForeignKey("LectureId")]
		public virtual Lecture Lecture { get; set; }
	}
}
