using backend.Models;
using backend.Types;
using System;
using System.Collections.Generic;
using Xunit;

namespace backend_test
{
    public class UnitTest
    {
        [Fact]
        public void assertEvent()
        {
            Event e = new Event()
            {
                Title = "Rust Programming",
                Description = "Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety.",
                Start = new DateTime(2018, 04, 06, 9, 0, 0),
                End = new DateTime(2018, 04, 06, 16, 0, 0),
            };

            Assert.Equal("Rust Programming", e.Title);
            Assert.Equal("Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety.", e.Description);
            Assert.Equal(new DateTime(2018, 04, 06, 9, 0, 0), e.Start);
            Assert.Equal(new DateTime(2018, 04, 06, 16, 0, 0), e.End);
        }

        [Fact]
        public void assertLocation()
        {
            Location l = new Location()
            {
                Name = "The Hague University",
                Description = "Main building of the Hague University in The Hague",
                Latitude = 10.00,
                Longitude = 20.00,
            };

            Assert.Equal("The Hague University", l.Name);
            Assert.Equal("Main building of the Hague University in The Hague", l.Description);
            Assert.Equal(10.00, l.Latitude);
            Assert.Equal(20.00, l.Longitude);
        }

        [Fact]
        public void assertUser()
        {
            User u = new User()
            {
                FirstName = "John",
                LastName = "Doe",
                Locale = "en_US",
                Type = backend.Types.Type.Student,
                Role = Role.Basic,
                Deleted = false,
            };

            Assert.Equal("John", u.FirstName);
            Assert.Equal("Doe", u.LastName);
            Assert.Equal("en_US", u.Locale);
            Assert.Equal(backend.Types.Type.Student, u.Type);
            Assert.Equal(Role.Basic, u.Role);
            Assert.False(u.Deleted);
        }

        [Fact]
        public void assertGroup()
        {
            Event e = new Event()
            {
                Title = "Test title",
                Description = "Test description",
                Start = new DateTime(2018, 4, 6, 9, 0, 0),
                End = new DateTime(2018, 4, 6, 9, 0, 0),
            };
            
            User u1 = new User()
            {
                FirstName = "Elliot",
                LastName = "Alderson",
                Type = backend.Types.Type.Student,
                Role = Role.Basic,
            };

            User u2 = new User()
            {
                FirstName = "Darlene",
                LastName = "Alderson",
                Type = backend.Types.Type.Student,
                Role = Role.Basic,
            };
            
            EventOwner eo1  = new EventOwner()
            {
                Event = e,
                People = u1,
            };
            
            EventAttendee ea1 = new EventAttendee()
            {
                Event = e,
                People = u1,
            };
            
            EventAttendee ea2 = new EventAttendee()
            {
                Event = e,
                People = u2,
            };

            Group g = new Group()
            {
                Name = "Linux Hackers",
                EventsOwn = new List<EventOwner> {eo1},
                EventsAttend = new List<EventAttendee> {ea1, ea2},
            };

            Assert.Equal("Linux Hackers", g.Name);
            Assert.Equal(1, g.EventsOwn.Count);
            Assert.Equal(2, g.EventsAttend.Count);
        }

        [Fact]
        public void assertPeople()
        {
            User tim    = new User() { FirstName = "Tim",    LastName = "Visée"      };
            User fleur  = new User() { FirstName = "Fleur",  LastName = "Arkesteijn" };
            User simon  = new User() { FirstName = "Simon",  LastName = "Haasnoot"   };
            User dennis = new User() { FirstName = "Dennis", LastName = "Volkering"  };
            User nathan = new User() { FirstName = "Nathan", LastName = "Bakhuijzen" };
            
            Group beunit = new Group();
            beunit.Name = "BeunIT";
        }
    }
}
