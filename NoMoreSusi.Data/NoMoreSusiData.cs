using System;
using System.Collections.Generic;
using System.Data.Entity;
using NoMoreSusi.Data.Interfaces;
using NoMoreSusi.Data.Repository;
using NoMoreSusi.Models;

namespace NoMoreSusi.Data
{
	public class NoMoreSusiData : INoMoreSusiData
	{
		private readonly DbContext _context;

		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

		public NoMoreSusiData(DbContext context)
		{
			this._context = context;
		}

		public IRepository<User> Users
		{
			get
			{
				return this.GetRepository<User>();
			}
		}

		public IRepository<Lecture> Lectures
		{
			get
			{
				return this.GetRepository<Lecture>();
			}
		}

		public IRepository<Student> Students
		{
			get
			{
				return this.GetRepository<Student>();
			}
		}

		public IRepository<Room> Rooms
		{
			get
			{
				return this.GetRepository<Room>();
			}
		}

		public IRepository<Teacher> Teachers
		{
			get
			{
				return this.GetRepository<Teacher>();
			}
		}

		public DbContext Context
		{
			get
			{
				return this._context;
			}
		}

		/// <summary>
		/// Saves all changes made in this context to the underlying database.
		/// </summary>
		/// <returns>
		/// The number of objects written to the underlying database.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
		public int SaveChanges()
		{
			return this._context.SaveChanges();
		}

		public void Dispose()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._context != null)
				{
					this._context.Dispose();
				}
			}
		}

		private IRepository<T> GetRepository<T>() where T : class
		{
			if (!this._repositories.ContainsKey(typeof(T)))
			{
				var type = typeof(GenericRepository<T>);

				this._repositories.Add(typeof(T), Activator.CreateInstance(type, this._context));
			}

			return (IRepository<T>)this._repositories[typeof(T)];
		}
	}
}
