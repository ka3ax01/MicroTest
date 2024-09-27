using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Interfaces;
using EmployeeService.Infrastructure.Data;
namespace EmployeeService.Infrastructure.Repositories{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext _context;

        public DepartmentRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.Include(d => d.Employees)
                                            .FirstOrDefaultAsync(d => d.ID == id);
        }

        public async Task<Department> GetDepartmentByNameAsync(string name){
            return await _context.Departments.FirstOrDefaultAsync(e => e.Name == name);
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(int pageNumber, int pageSize, string search = null){
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(search)){
                query = query.Where(e => e.Name.Contains(search));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}