using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domain.Entities{
    public class Department{
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? ManagerID { get; set; }
        [Required]
        public Employee Manager { get; set; }
        
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
