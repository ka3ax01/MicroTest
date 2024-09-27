
using EmployeeService.Domain.Entities;

namespace EmployeeService.Domain.Interfaces{
    public interface IDepartmentRepository{
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentByNameAsync(string Name);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(int pageNumber, int pageSize, string search = null);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}
