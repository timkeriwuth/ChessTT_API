using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labo.DAL.Configurations
{
    internal class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100);

            builder.Property(t => t.Location)
                .HasMaxLength(100);

            builder.Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}

