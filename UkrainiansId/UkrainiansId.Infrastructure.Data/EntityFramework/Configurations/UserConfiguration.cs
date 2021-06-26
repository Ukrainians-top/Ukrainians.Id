using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UkrainiansId.Domain.Models;
namespace UkrainiansId.Infrastructure.Data.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new
            {
                x.Username,
                x.Firstname,
                x.Lastname,
                x.CreatedAt
            });
        }
    }
}