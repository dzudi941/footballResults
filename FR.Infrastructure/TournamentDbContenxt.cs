using FR.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FR.Infrastructure
{
    public class TournamentDbContenxt : DbContext
    {
        public TournamentDbContenxt(DbContextOptions<TournamentDbContenxt> options)
      : base(options)
        { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
