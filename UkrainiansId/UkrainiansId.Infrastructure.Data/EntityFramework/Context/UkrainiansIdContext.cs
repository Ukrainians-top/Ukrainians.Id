using Microsoft.EntityFrameworkCore;
using UkrainiansId.Domain.Models;
using UkrainiansId.Infrastructure.Data.EntityFramework.Configurations;
namespace UkrainiansId.Infrastructure.Data.EntityFramework.Context
{
    public class UkrainiansIdContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthenticationData> AuthenticationData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfiguration());
        }
    }
}