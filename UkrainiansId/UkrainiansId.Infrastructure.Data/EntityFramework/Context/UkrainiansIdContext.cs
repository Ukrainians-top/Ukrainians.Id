using Microsoft.EntityFrameworkCore;
using UkrainiansId.Domain.Models;

namespace UkrainiansId.Infrastructure.Data.EntityFramework.Context
{
    public class UkrainiansIdContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}