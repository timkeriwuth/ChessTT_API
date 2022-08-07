using Labo.DAL.Configurations;
using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labo.DAL.Contexts
{
    public class TournamentContext: DbContext
    {
        public TournamentContext(DbContextOptions options): base(options) { }

        public DbSet<Tournament> Tournaments => Set<Tournament>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TournamentConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
