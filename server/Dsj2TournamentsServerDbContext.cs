using Dsj2TournamentsServer.Models;
using Dsj2TournamentsServer.Models.Tournament;
using Microsoft.EntityFrameworkCore;

namespace Dsj2TournamentsServer
{
    public class Dsj2TournamentsServerDbContext : DbContext
    {
        public Dsj2TournamentsServerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Jump> Jumps { get; set; }
        public DbSet<Hill> Hills { get; set; }
        public DbSet<User> Users { get; set; }
    }
}