using System.IO;
using EmployeeAPI.Controllers;
using EmployeeAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeeAPITests.Controllers
{
    public class EmployeeControllerTests : System.IDisposable
    {
        private const string TestFileName = "EMPLOYEE.DAT";

        public EmployeeControllerTests()
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
        }

        public void Dispose()
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
        }

        [Fact]
        public void Register_Should_Return_Ok_When_Valid_Request()
        {
            // Arrange
            var controller = new EmployeeController();
            var request = new EmployeeRequest
            {
                EmployeeId = "1234",
                Name = "John Doe",
                Age = "30",
                Address = "123 Main St"
            };

            // Act
            var result = controller.Register(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Employee record added", result.Value);

            // Verify that the record is written to file
            Assert.True(File.Exists(TestFileName));
            var content = File.ReadAllText(TestFileName);
            Assert.Contains("1234|John Doe|30|123 Main St", content);
        }

        [Fact]
        public void Register_Should_Return_BadRequest_For_Invalid_Request()
        {
            // Arrange - using an invalid EmployeeId
            var controller = new EmployeeController();
            var request = new EmployeeRequest
            {
                EmployeeId = "12", // Invalid
                Name = "John Doe",
                Age = "30",
                Address = "123 Main St"
            };

            // Act
            var result = controller.Register(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("o ID deve conter 4 dígitos numéricos.", result.Value);

            // Ensure that no file has been created
            Assert.False(File.Exists(TestFileName));
        }
    }
}
