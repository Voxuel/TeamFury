using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace TeamFury_API.Data
{
	public class AppDbContext : DbContext
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}
