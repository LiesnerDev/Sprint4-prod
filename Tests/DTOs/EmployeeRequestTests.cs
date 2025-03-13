using EmployeeAPI.DTOs;
using Xunit;

namespace EmployeeAPITests.DTOs
{
    public class EmployeeRequestTests
    {
        [Fact]
        public void EmployeeRequest_Should_Set_Properties_Correctly()
        {
            // Arrange
            var request = new EmployeeRequest
            {
                EmployeeId = "5678",
                Name = "Jane Smith",
                Age = "25",
                Address = "456 Elm St"
            };

            // Act & Assert
            Assert.Equal("5678", request.EmployeeId);
            Assert.Equal("Jane Smith", request.Name);
            Assert.Equal("25", request.Age);
            Assert.Equal("456 Elm St", request.Address);
        }
    }
}
