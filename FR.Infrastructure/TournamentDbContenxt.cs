using FR.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FR.Infrastructure
{
    public class TournamentDbContenxt : DbContext
    {
        public TournamentDbContenxt(DbContextOptions<TournamentDbContenxt> options)
      : base(options)
        { }

        //public DbSet<Tournament> tournaments;
        public DbSet<Group> Groups { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
