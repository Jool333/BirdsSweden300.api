using Microsoft.EntityFrameworkCore;
using BirdsSweden300.api.Entities;

namespace BirdsSweden300.api.Data
{
    public class BirdsContext : DbContext
    {
        public DbSet<Bird> Birds { get; set; }
        public BirdsContext(DbContextOptions options) : base(options){}
    }
}