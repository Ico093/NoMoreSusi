using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoMoreSusi.Models
{
    public class Teacher
    {
        public Teacher()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
