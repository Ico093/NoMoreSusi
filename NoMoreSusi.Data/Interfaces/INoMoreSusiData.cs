using System.Data.Entity;
using NoMoreSusi.Data.Repository;
using NoMoreSusi.Models;

namespace NoMoreSusi.Data.Interfaces
{
	public interface INoMoreSusiData
	{
		IRepository<User> Users { get; }

		IRepository<Lecture> Lectures { get; }

		IRepository<Student> Students { get; }

		IRepository<Room> Rooms { get; }

		IRepository<Teacher> Teachers { get; }

		DbContext Context { get; }

		int SaveChanges();

		void Dispose();
	}
}
