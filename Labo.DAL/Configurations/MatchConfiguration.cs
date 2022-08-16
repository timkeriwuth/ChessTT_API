using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labo.DAL.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.Property(m => m.Result).HasConversion<string>();
            builder.HasOne(m => m.White).WithMany(u => u.MatchesAsWhite).HasForeignKey(m => m.WhiteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Black).WithMany(u => u.MatchesAsBlack).HasForeignKey(m => m.BlackId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
