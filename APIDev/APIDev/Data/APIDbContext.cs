
using APIDev.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIDev.Data
{
    public class APIDbContext: DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options): base(options)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walk { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
