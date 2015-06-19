using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoMoreSusi.Models
{
    public class Lecture
    {
        public Lecture()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DayOfWeek Day { get; set; }

        public int Hour { get; set; }

        public int Length { get; set; }

        public Course Course { get; set; }

        public int TeacherId { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
