using backend.Models;
using backend.Types;
using System;
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
                Start = new DateTime(2018, 04, 06, 9, 0, 0, 0),
                End = new DateTime(2018, 04, 06, 16, 0, 0, 0),
            };

            Assert.Equal("Rust Programming", e.Title);
            Assert.Equal("Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety.", e.Description);
            Assert.Equal(new DateTime(2018, 04, 06, 9, 0, 0, 0), e.Start);
            Assert.Equal(new DateTime(2018, 04, 06, 16, 0, 0, 0), e.End);
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
                Role = backend.Types.Role.Basic,
                Deleted = false,
            };

            Assert.Equal("John", u.FirstName);
            Assert.Equal("Doe", u.LastName);
            Assert.Equal("en_US", u.Locale);
            Assert.Equal(backend.Types.Type.Student, u.Type);
            Assert.Equal(backend.Types.Role.Basic, u.Role);
            Assert.False(u.Deleted);
        }
    }
}
