using System;
using System.IO;
using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Xunit;

namespace EmployeeAPITests.Services
{
    public class EmployeeServiceTests : IDisposable
    {
        private readonly EmployeeService _employeeService;
        private const string TestFileName = "EMPLOYEE.DAT";
        
        public EmployeeServiceTests()
        {
            _employeeService = new EmployeeService();
            // Ensure EMPLOYEE.DAT is deleted before running tests
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
        }

        public void Dispose()
        {
            // Clean up after each test
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
        }

        [Fact]
        public void ValidateEmployee_Should_Return_Null_For_Valid_Employee()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "Valid Name",
                Age = "25",
                Address = "Valid Address"
            };

            // Act
            var result = _employeeService.ValidateEmployee(employee);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ValidateEmployee_Should_Return_Error_For_Invalid_EmployeeId()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "12", // Invalid: less than 4 digits
                Name = "Valid Name",
                Age = "25",
                Address = "Valid Address"
            };

            // Act
            var result = _employeeService.ValidateEmployee(employee);

            // Assert
            Assert.Equal("o ID deve conter 4 dígitos numéricos.", result);
        }

        [Fact]
        public void ValidateEmployee_Should_Return_Error_For_Invalid_Name()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "This name is definitely more than twenty characters",
                Age = "25",
                Address = "Valid Address"
            };

            // Act
            var result = _employeeService.ValidateEmployee(employee);

            // Assert
            Assert.Equal("o Nome deve conter até 20 caracteres.", result);
        }

        [Fact]
        public void ValidateEmployee_Should_Return_Error_For_Invalid_Age()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "Valid Name",
                Age = "123", // Invalid: more than 2 digits
                Address = "Valid Address"
            };

            // Act
            var result = _employeeService.ValidateEmployee(employee);

            // Assert
            Assert.Equal("a Idade deve conter 2 dígitos numéricos.", result);
        }

        [Fact]
        public void ValidateEmployee_Should_Return_Error_For_Invalid_Address()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "Valid Name",
                Age = "25",
                Address = "This address is definitely way too long to be accepted by the system"
            };

            // Act
            var result = _employeeService.ValidateEmployee(employee);

            // Assert
            Assert.Equal("o Endereço deve conter até 30 caracteres.", result);
        }

        [Fact]
        public void AddEmployee_Should_Append_Record_To_File()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = "1234",
                Name = "John Doe",
                Age = "30",
                Address = "123 Main St"
            };

            // Act
            _employeeService.AddEmployee(employee);

            // Assert
            Assert.True(File.Exists(TestFileName));
            var content = File.ReadAllText(TestFileName);
            Assert.Contains("1234|John Doe|30|123 Main St", content);
        }

        [Fact]
        public void AddEmployee_Should_Preserve_Existing_Records()
        {
            // Arrange
            var employee1 = new Employee
            {
                EmployeeId = "1234",
                Name = "John Doe",
                Age = "30",
                Address = "123 Main St"
            };
            var employee2 = new Employee
            {
                EmployeeId = "5678",
                Name = "Jane Smith",
                Age = "28",
                Address = "456 Elm St"
            };

            // Act
            _employeeService.AddEmployee(employee1);
            _employeeService.AddEmployee(employee2);

            // Assert
            var lines = File.ReadAllLines(TestFileName);
            Assert.Equal(2, lines.Length);
            Assert.Equal("1234|John Doe|30|123 Main St", lines[0]);
            Assert.Equal("5678|Jane Smith|28|456 Elm St", lines[1]);
        }
    }
}
