using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Interfaces;
namespace EmployeeService.Web.Controllers{
    [Route("api/Department/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search=null)
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync(pageNumber, pageSize, search);
            return Ok(departments);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetDepartmentByName(string name)
        {
           var department = await _departmentRepository.GetDepartmentByNameAsync(name);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            await _departmentRepository.AddDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.ID }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.ID)
            {
                return BadRequest();
            }

            await _departmentRepository.UpdateDepartmentAsync(department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}