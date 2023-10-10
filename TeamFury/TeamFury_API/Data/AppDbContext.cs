using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace TeamFury_API.Data
{
	public class AppDbContext : IdentityDbContext<IdentityUser>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<EmployeeRequest> EmployeesRequest { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<LeaveDays> LeaveDays { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                AdminID = 1,
                FirstName = "Patrik",
                LastName = "Skattberg",
                Email = "trolllovecookies@gmail.com",
                UserName = "Admin1",
                Password = "troll123"
            },
            new Admin()
            {
                AdminID = 2,
                FirstName = "Leo",
                LastName = "Fridh",
                Email = "leo.fridh@hotmail.com",
                UserName = "Admin2",
                Password = "MTG15"
			});
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeID = 1,
                FirstName = "Alfred",
                LastName = "Larsson",
                Email = "alfred.co95@gmail.com",
                UserName = "user1",
                Password = "AngularLover1"
            },
            new Employee()
            {
                EmployeeID = 2,
                FirstName = "Sebastian",
                LastName = "Gamboa",
                Email = "Seebastian.gamboa@gmail.com",
                UserName = "BigCockLover0",
                Password = "AssEater420"
			});
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser()
            {
	            Id = new Guid("6cef773a-6124-4182-a8ad-3567cd037ea7").ToString(),
	            Email = "trolllovecookies@gmail.com",
	            UserName = "Admin1",
	            PasswordHash = hasher.HashPassword(null, "Password")
            });
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
            {
	            new IdentityRole()
	            {
		            Id = "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
		            Name = "admin",
		            NormalizedName = "ADMIN",
	            }
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
	            RoleId = "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
	            UserId = "6cef773a-6124-4182-a8ad-3567cd037ea7"
            });
		}
	}
}
