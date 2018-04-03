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
        public new DbSet<User> Users { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<EventOwner> EventOwners { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }
        public DbSet<PeopleGroup> PeopleGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Event couplings
            builder.Entity<EventLocation>()
                .HasKey(x => new {x.EventId, x.LocationId});
            builder.Entity<EventLocation>()
                .HasOne(pt => pt.Event)
                .WithMany(p => p.Locations)
                .HasForeignKey(pt => pt.EventId);
            builder.Entity<EventLocation>()
                .HasOne(pt => pt.Location)
                .WithMany(t => t.Events)
                .HasForeignKey(pt => pt.LocationId);

            // Event owner couplings
            builder.Entity<EventOwner>()
                .HasKey(el => new { el.EventId, el.PeopleId});
            builder.Entity<EventOwner>()
                .HasOne(pt => pt.People)
                .WithMany(p => p.EventsOwn)
                .HasForeignKey(pt => pt.PeopleId);
            builder.Entity<EventOwner>()
                .HasOne(pt => pt.Event)
                .WithMany(t => t.Owners)
                .HasForeignKey(pt => pt.EventId);

            // Event attendee couplings
            builder.Entity<EventAttendee>()
                .HasKey(el => new { el.EventId, el.PeopleId});
            builder.Entity<EventAttendee>()
                .HasOne(el => el.People)
                .WithMany(e => e.EventsAttend)
                .HasForeignKey(x => x.PeopleId);
            builder.Entity<EventAttendee>()
                .HasOne(el => el.Event)
                .WithMany(e => e.Attendees)
                .HasForeignKey(x => x.EventId);

            // People group couplings
            builder.Entity<PeopleGroup>()
                .HasKey(el => new { el.GroupId, el.PeopleId});

            builder.Entity<PeopleGroup>()
                .HasOne(el => el.People)
                .WithMany(e => e.Groups)
                .HasForeignKey(x => x.PeopleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PeopleGroup>()
                .HasOne(el => el.Group)
                .WithMany(e => e.People)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}
