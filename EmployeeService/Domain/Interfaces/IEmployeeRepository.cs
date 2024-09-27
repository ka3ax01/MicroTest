
using EmployeeService.Domain.Entities;

namespace EmployeeService.Domain.Interfaces{
    public interface IEmployeeRepository{
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesBirthdayAsync();
        Task<Employee> GetEmployeeByNameAsync(string name);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize, string search = null);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
