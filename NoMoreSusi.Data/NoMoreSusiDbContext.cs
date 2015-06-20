using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoMoreSusi.Models;

namespace NoMoreSusi.Data
{
    public class NoMoreSusiDbContext : IdentityDbContext<User>
    {
        public NoMoreSusiDbContext()
            : base("NoMoreSusi", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Room> Rooms { get; set; }
        public virtual IDbSet<Lecture> Lectures { get; set; }
        public virtual IDbSet<Teacher> Teachers { get; set; }

        public static NoMoreSusiDbContext Create()
        {
            return new NoMoreSusiDbContext();
        }
    }
}
