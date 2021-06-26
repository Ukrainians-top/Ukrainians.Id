using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UkrainiansId.Domain.Models;
namespace UkrainiansId.Infrastructure.Data.EntityFramework.Configurations
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new
            {
                x.Name,
                x.ClientId,
                x.ClientSecret,
                x.ClientSecretIsRequired
            });
        }
    }
}