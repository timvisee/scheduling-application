using System;
using System.Collections.Generic;
using System.Linq;
using backend.App.Util;
using backend.Models;
using backend.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using SQLitePCL;
using Type = backend.Types.Type;

namespace backend.Data
{
    public static class DbBuilder
    {
        /// <summary>
        /// Rebuild the selected database.
        ///
        /// Warning: This overrides the current data available in the database.
        /// </summary>
        /// <param name="context"></param>
        public static void Rebuild(ApplicationDbContext context)
        {
            // Show a status message to the user
            LogUtils.Warning("Starting database initialization...");

            // Remove the database
            Console.WriteLine("Deleting database... (please be patient, this may take up to 20 seconds)");
            context.Database.EnsureDeleted();

            // Create the database
            Console.WriteLine("Provisioning new database... (please be patient, this may take up to 60 seconds)");
            context.Database.EnsureCreated();

//            var newuser = new ApplicationUser
//            {
//                Email = "beun@beun.it",
//                NormalizedEmail = "BEUN@BEUN.IT",
//                UserName = "beun@beun.it",
//                NormalizedUserName = "BEUN@BEUN.IT",
//                PhoneNumber = "0612345678",
//                EmailConfirmed = true,
//                PhoneNumberConfirmed = true,
//                SecurityStamp = Guid.NewGuid().ToString("D")
//            };
//
//            var password = new PasswordHasher<ApplicationUser>();
//            var hashed = password.HashPassword(newuser, "beun");
//            newuser.PasswordHash = hashed;
//
//            var userStore = new UserStore<ApplicationUser>(context);
//            var result = userStore.CreateAsync(newuser);
//
//            // Wait for the actual result
//            result.Wait();

            LogUtils.Log("Generating seed data", ConsoleColor.Green);

            /**
             * USERS
             */
            var us = new User
            {
                FirstName = "Dennis",
                Infix = "",
                LastName = "Volkering",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us);

            var us3 = new User
            {
                FirstName = "Tim",
                Infix = "",
                LastName = "Visee",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us3);

            var us4 = new User
            {
                FirstName = "Simon",
                Infix = "",
                LastName = "Haasnoot",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us4);

            var us5 = new User
            {
                FirstName = "Nathan",
                Infix = "",
                LastName = "Bakhuijzen",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us5);

            var us6 = new User
            {
                FirstName = "Fleur",
                Infix = "",
                LastName = "",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us6);
            context.SaveChanges();

            /**
             * LOCATIONS
             */
            var loc = new Location
            {
                Name = "SL 6.54",
                Description = "",
                Latitude = 0.0,
                Longitude = 0.0
            };

            var loc1 = new Location
            {
                Name = "OV 1.49",
                Description = "College room",
                Latitude = 0.0,
                Longitude = 0.0
            };

            var loc2 = new Location
            {
                Name = "OV 1.51",
                Description = "College room",
                Latitude = 0.0,
                Longitude = 0.0
            };

            var loc3 = new Location
            {
                Name = "Meeting room",
                Description = "First floor",
                Latitude = 0.0,
                Longitude = 0.0
            };

            context.Locations.Add(loc);
            context.Locations.Add(loc1);
            context.Locations.Add(loc2);
            context.Locations.Add(loc3);

            /**
             * EVENTS
             */
            var ev = new Event
            {
                Start = new DateTime(2018, 4, 16, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 16, 14, 30, 0, 0),
                Description = "",
                Title = "Programming in PHP"
            };
            context.Events.Add(ev);

            var ev2 = new Event
            {
                Start = new DateTime(2018, 4, 17, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 14, 30, 0, 0),
                Description = "",
                Title = "Object Oriented programming"
            };
            context.Events.Add(ev2);

            var ev3 = new Event
            {
                Start = new DateTime(2018, 4, 17, 10, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 11, 30, 0, 0),
                Description = "",
                Title = "Databases"
            };
            context.Events.Add(ev3);

            var ev4 = new Event
            {
                Start = new DateTime(2018, 4, 17, 8, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 9, 30, 0, 0),
                Description = "",
                Title = "How to deal with ASP.NET"
            };
            context.Events.Add(ev4);

            var ev5 = new Event
            {
                Start = new DateTime(2018, 4, 18, 8, 0, 0, 0),
                End = new DateTime(2018, 4, 18, 9, 30, 0, 0),
                Description = "",
                Title = "DevOps"
            };
            context.Events.Add(ev5);

            var ev6 = new Event
            {
                Start = new DateTime(2018, 4, 18, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 18, 13, 30, 0, 0),
                Description = "",
                Title = "Rust for beginners"
            };
            context.Events.Add(ev6);

            var ev7 = new Event
            {
                Start = new DateTime(2018, 4, 19, 15, 50, 0, 0),
                End = new DateTime(2018, 4, 19, 17, 0, 0, 0),
                Description = "",
                Title = "Rust - Advanced course"
            };
            context.Events.Add(ev7);

            context.SaveChanges();

            var el = new EventLocation
            {
                Event = ev,
                Location = loc
            };
            var el1 = new EventLocation
            {
                Event = ev,
                Location = loc1
            };
            var el2 = new EventLocation
            {
                Event = ev,
                Location = loc2
            };
            var el3 = new EventLocation
            {
                Event = ev2,
                Location = loc2
            };
            var el4 = new EventLocation
            {
                Event = ev3,
                Location = loc3
            };
            var el5 = new EventLocation
            {
                Event = ev3,
                Location = loc
            };
            var el6 = new EventLocation
            {
                Event = ev4,
                Location = loc1
            };
            var el7 = new EventLocation
            {
                Event = ev5,
                Location = loc
            };
            var el8 = new EventLocation
            {
                Event = ev6,
                Location = loc
            };
            var el9 = new EventLocation
            {
                Event = ev7,
                Location = loc1
            };

            context.EventLocations.Add(el);
            context.EventLocations.Add(el1);
            context.EventLocations.Add(el2);
            context.EventLocations.Add(el3);
            context.EventLocations.Add(el4);
            context.EventLocations.Add(el5);
            context.EventLocations.Add(el6);
            context.EventLocations.Add(el7);
            context.EventLocations.Add(el8);
            context.EventLocations.Add(el9);

            context.SaveChanges();

            var ea = new EventAttendee
            {
                Event = ev,
                People = us
            };
            var ea1 = new EventAttendee
            {
                Event = ev,
                People = us3
            };
            var ea2 = new EventAttendee
            {
                Event = ev,
                People = us4
            };

            context.EventAttendees.Add(ea);
            context.EventAttendees.Add(ea1);
            context.EventAttendees.Add(ea2);

            context.SaveChanges();


            // Show a success message
            LogUtils.Success("Database built!");

            // Save the changes to the database context
            context.SaveChanges();

            // Exit the application if configured, when the built process is complete
            if (Program.AppConfig != null && Program.AppConfig.DatabaseExitAfterReset)
            {
                // Tell the user the application will exit
                LogUtils.Warning("The database has been built. The application will now exit.");

                // Actually exit the application
                Environment.Exit(0);
            }
        }
    }
}
