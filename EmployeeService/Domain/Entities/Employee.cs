using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domain.Entities{
    public class Employee{
        [Key]
        public int ID { get; set; } //default incremental parameter
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; } // should be pulled remotely
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmployeeStatus { get; set; } // Active, Sick, Vacation, BuisnessTrip
        [Required]
        public string Position { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public Department Department { get; set; }
    }
}