using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

// Note: For testing Program.cs in a minimal API, ensure that the Program class is not internal.
// If necessary, add a 'public partial class Program {}' declaration in Program.cs.

namespace EmployeeAPITests
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProgramTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task App_Starts_And_Returns_NotFound_For_Invalid_Endpoint()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/non-existent-endpoint");

            // Assert
            // Expecting 404 Not Found for an endpoint that does not exist
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
