using EmployeeAPI.Models;
using Xunit;

namespace EmployeeAPITests.Models
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_Should_Set_Properties_Correctly()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "John Doe",
                Age = "30",
                Address = "123 Main St"
            };

            // Act & Assert
            Assert.Equal("1234", employee.EmployeeId);
            Assert.Equal("John Doe", employee.Name);
            Assert.Equal("30", employee.Age);
            Assert.Equal("123 Main St", employee.Address);
        }
    }
}
