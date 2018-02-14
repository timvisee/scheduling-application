using Microsoft.EntityFrameworkCore;

namespace webapp.Models
{
    public class DbEntity : DbContext
    {
        public DbEntity(DbContextOptions<DbEntity> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<User> Users { get; set; }
    }
}