using System.Collections.Generic;

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

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
