using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Interfaces;
namespace EmployeeService.Web.Controllers{
    [Route("api/Employee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search=null)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize, search);
            return Ok(employees);
        }

        [HttpGet("birthdays")]
        public async Task<IActionResult> GetEmployeesWithBirthdayToday()
        {
            var employees = await _employeeRepository.GetEmployeesBirthdayAsync();
            return Ok(employees);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
           var employee = await _employeeRepository.GetEmployeeByNameAsync(name);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            try
            {
                await _employeeRepository.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.ID }, employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}