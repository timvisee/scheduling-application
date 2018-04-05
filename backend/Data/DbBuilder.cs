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

            LogUtils.Log("Generating seed data", ConsoleColor.Green);


            var roleAdmin = new IdentityRole {
                Name = "ADMIN",
                NormalizedName= "ADMIN"
            };

            var roleBasic = new IdentityRole {
                Name = "BASIC",
                NormalizedName= "BASIC"
            };

            var roleElevated = new IdentityRole {
                Name = "ELEVATED",
                NormalizedName= "ELEVATED"
            };

            var roleReadOnly = new IdentityRole {
                Name = "READONLY",
                NormalizedName= "READONLY"
            };

            context.Roles.Add(roleAdmin);
            context.Roles.Add(roleBasic);
            context.Roles.Add(roleElevated);
            context.Roles.Add(roleReadOnly);

            context.SaveChanges();

            /**
             * USERS
             */
            LogUtils.Info("Creating new users");
            var us = new User
            {
                FirstName = "Dennis",
                Infix = "",
                LastName = "Volkering",
                Locale = "NL",
                Role = Role.Admin,
                Type = Type.Student
            };

            var us3 = new User
            {
                FirstName = "Tim",
                Infix = "",
                LastName = "Visee",
                Locale = "NL",
                Role = Role.Admin,
                Type = Type.Student
            };

            var us4 = new User
            {
                FirstName = "Simon",
                Infix = "",
                LastName = "Haasnoot",
                Locale = "NL",
                Role = Role.Admin,
                Type = Type.Student
            };

            var us5 = new User
            {
                FirstName = "Nathan",
                Infix = "",
                LastName = "Bakhuijzen",
                Locale = "NL",
                Role = Role.Admin,
                Type = Type.Student
            };

            var us6 = new User
            {
                FirstName = "Fleur",
                Infix = "",
                LastName = "",
                Locale = "NL",
                Role = Role.Admin,
                Type = Type.Student
            };
            context.Users.Add(us);
            context.Users.Add(us3);
            context.Users.Add(us4);
            context.Users.Add(us5);
            context.Users.Add(us6);
            context.SaveChanges();

            /**
             * LOCATIONS
             */
            LogUtils.Info("Creating new Locations");
            var loc = new Location
            {
                Name = "SL 6.54",
                Description = "",
                Latitude = 52.06689529502207,
                Longitude = 4.323538541793824
            };

            var loc1 = new Location
            {
                Name = "OV 1.49",
                Description = "College room",
                Latitude = 52.06738995145362,
                Longitude = 4.325249791145326
            };

            var loc2 = new Location
            {
                Name = "OV 1.51",
                Description = "College room",
                Latitude = 52.06733059297113,
                Longitude = 4.325287342071534
            };

            var loc3 = new Location
            {
                Name = "Meeting room",
                Description = "First floor",
                Latitude = 52.06713932621309,
                Longitude = 4.324176907539369
            };

            context.Locations.Add(loc);
            context.Locations.Add(loc1);
            context.Locations.Add(loc2);
            context.Locations.Add(loc3);

            /**
             * EVENTS
             */
            LogUtils.Info("Creating new Events");
            var ev = new Event
            {
                Start = new DateTime(2018, 4, 16, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 16, 14, 30, 0, 0),
                Description = "",
                Title = "Programming in PHP"
            };

            var ev2 = new Event
            {
                Start = new DateTime(2018, 4, 17, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 14, 30, 0, 0),
                Description = "",
                Title = "Object Oriented programming"
            };

            var ev3 = new Event
            {
                Start = new DateTime(2018, 4, 17, 10, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 11, 30, 0, 0),
                Description = "",
                Title = "Databases"
            };

            var ev4 = new Event
            {
                Start = new DateTime(2018, 4, 17, 8, 0, 0, 0),
                End = new DateTime(2018, 4, 17, 9, 30, 0, 0),
                Description = "Lazy loading, upgrading to version 2.1, etc",
                Title = "How to deal with ASP.NET"
            };

            var ev5 = new Event
            {
                Start = new DateTime(2018, 4, 18, 8, 0, 0, 0),
                End = new DateTime(2018, 4, 18, 9, 30, 0, 0),
                Description = "",
                Title = "DevOps"
            };

            var ev6 = new Event
            {
                Start = new DateTime(2018, 4, 18, 12, 0, 0, 0),
                End = new DateTime(2018, 4, 18, 13, 30, 0, 0),
                Description = "The starters course for Rust programming",
                Title = "Rust for beginners"
            };

            var ev7 = new Event
            {
                Start = new DateTime(2018, 4, 19, 15, 50, 0, 0),
                End = new DateTime(2018, 4, 19, 17, 0, 0, 0),
                Description = "The advanced course of Rust programming",
                Title = "Rust - Advanced course"
            };


            context.Events.Add(ev);
            context.Events.Add(ev2);
            context.Events.Add(ev3);
            context.Events.Add(ev4);
            context.Events.Add(ev5);
            context.Events.Add(ev6);
            context.Events.Add(ev7);

            context.SaveChanges();

            /**
             * GROUP
             */
            LogUtils.Info("Creating a new group");
            var gr = new Group {Name = "Beun IT"};
            context.Groups.Add(gr);
            context.SaveChanges();

            var pg = new PeopleGroup {People = us, Group = gr};
            var pg1 = new PeopleGroup {People = us3, Group = gr};
            var pg2 = new PeopleGroup {People = us4, Group = gr};
            var pg3 = new PeopleGroup {People = us5, Group = gr};
            var pg4 = new PeopleGroup {People = us6, Group = gr};

            context.PeopleGroups.Add(pg);
            context.PeopleGroups.Add(pg1);
            context.PeopleGroups.Add(pg2);
            context.PeopleGroups.Add(pg3);
            context.PeopleGroups.Add(pg4);
            context.SaveChanges();

            /**
             * EVENTLOCATIONS
             */
            LogUtils.Info("Creating coupling between Events & Locations");
            var el = new EventLocation {Event = ev, Location = loc};
            var el1 = new EventLocation {Event = ev, Location = loc1};
            var el2 = new EventLocation {Event = ev, Location = loc2};
            var el3 = new EventLocation {Event = ev2, Location = loc2};
            var el4 = new EventLocation {Event = ev3, Location = loc3};
            var el5 = new EventLocation {Event = ev3, Location = loc};
            var el6 = new EventLocation {Event = ev4, Location = loc1};
            var el7 = new EventLocation {Event = ev5, Location = loc};
            var el8 = new EventLocation {Event = ev6, Location = loc};
            var el9 = new EventLocation {Event = ev7, Location = loc1};

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

            /**
             * EVENTATTENDEES
             */
            LogUtils.Info("Creating coupling between Events & Attendees");
            var ea = new EventAttendee {Event = ev, People = us};
            var ea1 = new EventAttendee {Event = ev, People = us3};
            var ea2 = new EventAttendee {Event = ev, People = us4};
            var ea3 = new EventAttendee {Event = ev2, People = us6};
            var ea4 = new EventAttendee {Event = ev2, People = us4};
            var ea5 = new EventAttendee {Event = ev3, People = us6};
            var ea6 = new EventAttendee {Event = ev3, People = gr};
            var ea7 = new EventAttendee {Event = ev4, People = us6};
            var ea8 = new EventAttendee {Event = ev5, People = gr};
            var ea9 = new EventAttendee {Event = ev6, People = us3};
            var ea10 = new EventAttendee {Event = ev6, People = us5};
            var ea11 = new EventAttendee {Event = ev7, People = us};
            var ea12 = new EventAttendee {Event = ev7, People = us4};

            context.EventAttendees.Add(ea);
            context.EventAttendees.Add(ea1);
            context.EventAttendees.Add(ea2);
            context.EventAttendees.Add(ea3);
            context.EventAttendees.Add(ea4);
            context.EventAttendees.Add(ea5);
            context.EventAttendees.Add(ea6);
            context.EventAttendees.Add(ea7);
            context.EventAttendees.Add(ea8);
            context.EventAttendees.Add(ea9);
            context.EventAttendees.Add(ea10);
            context.EventAttendees.Add(ea11);
            context.EventAttendees.Add(ea12);

            context.SaveChanges();

            LogUtils.Info("Creating new ApplicationUser");
            // User 1
            var newuser = new ApplicationUser
            {
                Email = "dennisvolkering@hotmail.com",
                NormalizedEmail = "dennisvolkering@hotmail.com",
                UserName = "dennisvolkering@hotmail.com",
                NormalizedUserName = "dennisvolkering@hotmail.com",
                PhoneNumber = "0612345678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                User = us
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(newuser, "beun");
            newuser.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(context);
            var result = userStore.CreateAsync(newuser);

            // Wait for the actual result
            result.Wait();

            // User 2
            var newuser2 = new ApplicationUser
            {
                Email = "tim@visee.me",
                NormalizedEmail = "tim@visee.me",
                UserName = "tim@visee.me",
                NormalizedUserName = "tim@visee.me",
                PhoneNumber = "0612345678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                User = us3
            };

            var password2 = new PasswordHasher<ApplicationUser>();
            var hashed2 = password2.HashPassword(newuser2, "beun");
            newuser2.PasswordHash = hashed2;

            var userStore2 = new UserStore<ApplicationUser>(context);
            var result2 = userStore2.CreateAsync(newuser2);

            // Wait for the actual result
            result2.Wait();


            // User 3
            var newuser3 = new ApplicationUser
            {
                Email = "simonhaasnoot@hotmail.com",
                NormalizedEmail = "simonhaasnoot@hotmail.com",
                UserName = "simonhaasnoot@hotmail.com",
                NormalizedUserName = "simonhaasnoot@hotmail.com",
                PhoneNumber = "0612345678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                User = us4
            };

            var password3 = new PasswordHasher<ApplicationUser>();
            var hashed3 = password3.HashPassword(newuser3, "beun");
            newuser3.PasswordHash = hashed3;

            var userStore3 = new UserStore<ApplicationUser>(context);
            var result3 = userStore3.CreateAsync(newuser3);

            // Wait for the actual result
            result3.Wait();


            // User 4
            var newuser4 = new ApplicationUser
            {
                Email = "nathanbakhuijzen@gmail.com",
                NormalizedEmail = "nathanbakhuijzen@gmail.com",
                UserName = "nathanbakhuijzen@gmail.com",
                NormalizedUserName = "nathanbakhuijzen@gmail.com",
                PhoneNumber = "0612445678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                User = us5
            };

            var password4 = new PasswordHasher<ApplicationUser>();
            var hashed4 = password4.HashPassword(newuser4, "beun");
            newuser4.PasswordHash = hashed4;

            var userStore4 = new UserStore<ApplicationUser>(context);
            var result4 = userStore4.CreateAsync(newuser4);

            // Wait for the actual result
            result4.Wait();

            // User 5
            var newuser5 = new ApplicationUser
            {
                Email = "fleurarkesteijn@hotmail.com",
                NormalizedEmail = "fleurarkesteijn@hotmail.com",
                UserName = "fleurarkesteijn@hotmail.com",
                NormalizedUserName = "fleurarkesteijn@hotmail.com",
                PhoneNumber = "0612555678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                User = us6
            };

            var password5 = new PasswordHasher<ApplicationUser>();
            var hashed5 = password5.HashPassword(newuser5, "beun");
            newuser5.PasswordHash = hashed5;

            var userStore5 = new UserStore<ApplicationUser>(context);
            var result5 = userStore5.CreateAsync(newuser5);

            // Wait for the actual result
            result5.Wait();

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
