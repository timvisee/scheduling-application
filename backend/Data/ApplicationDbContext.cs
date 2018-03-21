using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<EventLocation> EventLocations { get; set; }
        public DbSet<PeopleGroup> PeopleGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Event couplings
            builder.Entity<EventLocation>()
                .HasKey(el => new { el.EventId, el.LocationId});
            builder.Entity<EventLocation>()
                .HasOne(el => el.Location)
                .WithMany(e => e.Events)
                .HasForeignKey(el => el.LocationId);
            builder.Entity<EventLocation>()
                .HasOne(el => el.Event)
                .WithMany(e => e.Locations)
                .HasForeignKey(el => el.EventId);

            // People group couplings
            builder.Entity<PeopleGroup>()
                .HasKey(el => new { el.GroupId, el.PeopleId});
            builder.Entity<PeopleGroup>()
                .HasOne(el => el.People)
                .WithMany(e => e.Groups)
                .HasForeignKey(el => el.GroupId);
            builder.Entity<PeopleGroup>()
                .HasOne(el => el.Group)
                .WithMany(e => e.Peoples)
                .HasForeignKey("Id");

            base.OnModelCreating(builder);
        }
    }
}
