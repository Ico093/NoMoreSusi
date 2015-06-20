using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoMoreSusi.Common;
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

            base.Seed(context);
			}

		private void SeedRoles(NoMoreSusiDbContext context)
		{
            var adminRole = new IdentityRole
			{
                Name = GlobalConstants.AdminRoleName,
            };

            var teacherRole = new IdentityRole
            {
                Name = GlobalConstants.TeacherRoleName,
            };

            var studentRole = new IdentityRole
            {
                Name = GlobalConstants.StudentRoleName,
			};

            context.Roles.Add(adminRole);
            context.Roles.Add(teacherRole);
            context.Roles.Add(studentRole);
            context.SaveChanges();
		}

		private void SeedUsers(NoMoreSusiDbContext context)
		{
			var userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

			var user = new User
			{
                UserName = GlobalConstants.AdminUserName,
                Email = GlobalConstants.AdminEmail
			};

			userManager.Create(user, GlobalConstants.AdminPassword);
			userManager.AddToRole(user.Id, GlobalConstants.AdminRoleName);
		}
	}
}