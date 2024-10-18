using HolidayHomeProject.DataAccessLayer.Dto;
using Microsoft.EntityFrameworkCore;

namespace HolidayHomeProject.DataAccessLayer.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<UserAccount> UserAccounts { get; set; }
        // DbSet for HostBio
        public DbSet<HostBio> Hosts { get; set; }

        // DbSet for House
        public DbSet<House> Houses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<HostBio>()
                .HasMany(h => h.Houses)
                .WithOne(h => h.Host)
                .HasForeignKey(h => h.HostId);
            base.OnModelCreating(modelBuilder);
		}
	}
}
