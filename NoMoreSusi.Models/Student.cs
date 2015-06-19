using System.Collections.Generic;

namespace NoMoreSusi.Models
{
    public class Student
    {
        public Student()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Course Course { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
