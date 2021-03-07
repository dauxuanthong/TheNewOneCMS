using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PostSys.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Publication> Publications { get; set; }

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}