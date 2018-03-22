using System;
using System.Collections.Generic;
using System.Linq;
using backend.App.Util;
using backend.Models;
using backend.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
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

            var newuser = new ApplicationUser
            {
                Email = "beun@beun.it",
                NormalizedEmail = "BEUN@BEUN.IT",
                UserName = "beun@beun.it",
                NormalizedUserName = "BEUN@BEUN.IT",
                PhoneNumber = "0612345678",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(newuser, "beun");
            newuser.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(context);
            var result = userStore.CreateAsync(newuser);

            // Wait for the actual result
            result.Wait();

            LogUtils.Log("Generating seed data", ConsoleColor.Green);


            /**
             * USERS
             */

            var us = new User
            {
                FirstName = "Bem ",
                Infix = "",
                LastName = "Test",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us);

            var us3 = new User
            {
                FirstName = "Bem ",
                Infix = "",
                LastName = "Test",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us3);

            var us4 = new User
            {
                FirstName = "Bem ",
                Infix = "",
                LastName = "Test",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us4);

            var us5 = new User
            {
                FirstName = "Bem ",
                Infix = "",
                LastName = "Test",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us5);

            var us6 = new User
            {
                FirstName = "Bem ",
                Infix = "",
                LastName = "Test",
                Locale = "NL",
                Role = Role.Basic,
                Type = Type.Student
            };
            context.Users.Add(us6);

            context.SaveChanges();

            List<People> userList = new List<People>();
            List<People> userList2 = new List<People>();

            userList.Add(us);
            userList.Add(us3);
            userList.Add(us4);
            userList.Add(us5);
            userList.Add(us6);

            userList2.Add(us);
            userList2.Add(us3);
            userList2.Add(us6);

            /**
             * EVENTS
             */
            var ev = new Event
            {
                Start = new DateTime(2018, 3, 19, 8, 0, 0, 0),
                End = new DateTime(2018, 3, 19, 9, 0, 0, 0),
                Description = "",
                Title = "Title of Event",
                Peoples = userList
            };
            context.Events.Add(ev);


            var ev2 = new Event
            {
                Start = new DateTime(2018, 3, 19, 11, 0, 0, 0),
                End = new DateTime(2018, 3, 19, 12, 0, 0, 0),
                Description = "",
                Title = "Title of Event",
                Peoples = userList2
            };
            context.Events.Add(ev2);

            var ev3 = new Event
            {
                Start = new DateTime(2018, 3, 19, 12, 0, 0, 0),
                End = new DateTime(2018, 3, 19, 13, 0, 0, 0),
                Description = "",
                Title = "Title of Event",
                Peoples = userList
            };
            context.Events.Add(ev3);

            var ev4 = new Event
            {
                Start = new DateTime(2018, 3, 20, 8, 0, 0, 0),
                End = new DateTime(2018, 3, 20, 9, 0, 0, 0),
                Description = "",
                Title = "Title of Event",
                Peoples = userList2
            };
            context.Events.Add(ev4);
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
