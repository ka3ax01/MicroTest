using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Interfaces;
using EmployeeService.Infrastructure.Data;
namespace EmployeeService.Infrastructure.Repositories{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.Include(e => e.Department)
                                        .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<Employee> GetEmployeeByNameAsync(string name){
            return await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == name);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize, string search = null){
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(search)){
                query = query.Where(e => e.FirstName.Contains(search));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesBirthdayAsync()
        {
            var today = DateTime.Today;
            return await _context.Employees.Where(e => e.BirthDate.Month == today.Month && e.BirthDate.Day == today.Day)
                                        .ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.ID == employee.DepartmentId);
            if (department == null)
            {
                throw new Exception("Department does not exist");
            }
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}