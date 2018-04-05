using backend.Models;
using Xunit;

namespace backend_test
{
    public class TestUnitTest
    {
        private readonly User user;


        [Fact]
        public void CreateInEventController()
        {
            // Create a NewsCategoryView (for the create function)
            Event ev = new Event()
            {
                Title = "test title"
            };
        }
    }
}
