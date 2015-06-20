using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoMoreSusi.Models;

namespace NoMoreSusi.Data.Migrations
{
	public sealed class Configuration : DbMigrationsConfiguration<NoMoreSusiDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(NoMoreSusiDbContext context)
		{
			if (!context.Roles.Any())
			{
				SeedRoles(context);
			}

			if (!context.Users.Any() && context.Roles.Any())
			{
				SeedUsers(context);
			}

			if (!context.Rooms.Any())
			{
				SeedRooms(context);
			}
		}

		private void SeedRoles(NoMoreSusiDbContext context)
		{
			var role = new IdentityRole
			{
				Name = "Administrator",
			};

			context.Roles.Add(role);
		}

		private void SeedUsers(NoMoreSusiDbContext context)
		{
			var userStore = new UserStore<User>(context);
			UserManager<User> um = new UserManager<User>(userStore);

			var user = new User
			{
				UserName = "admin",
				Email = "admin@admin.com"
			};

			var result = um.CreateAsync(user, "password").Result;
		}

		private void SeedRooms(NoMoreSusiDbContext context)
		{
			//just to check
		}
	}
}