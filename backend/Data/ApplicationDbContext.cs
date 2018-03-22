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
        public DbSet<People> People { get; set; }
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
                .HasForeignKey("Id");
            builder.Entity<EventLocation>()
                .HasOne(el => el.Event)
                .WithMany(e => e.Locations)
                .HasForeignKey("Id");

            // Event owner couplings
            builder.Entity<EventOwner>()
                .HasKey(el => new { el.EventId, el.PeopleId});
            builder.Entity<EventOwner>()
                .HasOne(el => el.People)
                .WithMany(e => e.EventsOwn)
                .HasForeignKey("Id");
            builder.Entity<EventOwner>()
                .HasOne(el => el.Event)
                .WithMany(e => e.Owners)
                .HasForeignKey("Id");

            // Event attendee couplings
            builder.Entity<EventAttendee>()
                .HasKey(el => new { el.EventId, el.PeopleId});
            builder.Entity<EventAttendee>()
                .HasOne(el => el.People)
                .WithMany(e => e.EventsAttend)
                .HasForeignKey("Id");
            builder.Entity<EventAttendee>()
                .HasOne(el => el.Event)
                .WithMany(e => e.Attendees)
                .HasForeignKey("Id");

            // People group couplings
            builder.Entity<PeopleGroup>()
                .HasKey(el => new { el.GroupId, el.PeopleId});
            builder.Entity<PeopleGroup>()
                .HasOne(el => el.People)
                .WithMany(e => e.Groups)
                .HasForeignKey("Id");
            builder.Entity<PeopleGroup>()
                .HasOne(el => el.Group)
                .WithMany(e => e.People)
                .HasForeignKey("Id");

            base.OnModelCreating(builder);
        }
    }
}
