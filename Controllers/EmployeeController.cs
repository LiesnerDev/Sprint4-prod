using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.DTOs;
using EmployeeAPI.Models;
using EmployeeAPI.Services;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController()
        {
            // Em um cenário real, use injeção de dependência
            _employeeService = new EmployeeService();
        }

        [HttpPost("register")]
        public IActionResult Register(EmployeeRequest request)
        {
            // Mapeia o DTO para o modelo de domínio
            var employee = new Employee
            {
                EmployeeId = request.EmployeeId,
                Name = request.Name,
                Age = request.Age,
                Address = request.Address
            };

            // Valida os dados do funcionário
            var validationError = _employeeService.ValidateEmployee(employee);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }

            // Armazena o registro no arquivo EMPLOYEE.DAT
            _employeeService.AddEmployee(employee);
            
            // Retorna a mensagem de confirmação
            return Ok("Employee record added");
        }
    }
}
