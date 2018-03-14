using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<EventLocation> EventLocations { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
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

            builder.Entity<UserGroup>()
                .HasKey(el => new { el.GroupId, el.UserId});

            builder.Entity<UserGroup>()
                .HasOne(el => el.User)
                .WithMany(e => e.Groups)
                .HasForeignKey(el => el.UserId);

            builder.Entity<UserGroup>()
                .HasOne(el => el.Group)
                .WithMany(e => e.Users)
                .HasForeignKey(el => el.GroupId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
