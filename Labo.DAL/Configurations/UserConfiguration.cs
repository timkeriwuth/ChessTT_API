using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToolBox.Security.Utils;

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

            builder.HasData(CreateAdmin());
        }

        private IEnumerable<User> CreateAdmin()
        {
            Guid salt = Guid.NewGuid();
            yield return new User
            {
                Id = Guid.NewGuid(),
                Username = "Checkmate",
                Email = "lykhun@gmail.com",
                Gender = DL.Enums.UserGender.Male,
                Role = DL.Enums.UserRole.Admin,
                BirthDate = new DateTime(1982, 5, 6),
                Elo = 1800,
                IsDeleted = false,
                Salt = salt,
                EncodedPassword = HashUtils.HashPassword("1234", salt)
            };
        }
    }
}
