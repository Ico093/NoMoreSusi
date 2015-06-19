using System.Collections.Generic;

namespace NoMoreSusi.Models
{
    public class Room
    {
        public Room()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }

        public Facultity Facultity { get; set; }

        public int Number { get; set; }

        public int PeopleCount { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
