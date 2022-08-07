using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labo.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username)
                .HasMaxLength(100);

            builder.Property(u => u.Gender)
                .HasConversion<string>();

            builder.Property(u => u.Role)
                .HasConversion<string>();

            builder.HasIndex(u => u.Username).IsUnique();

            builder.HasIndex(u => u.Salt).IsUnique();
        }
    }
}
