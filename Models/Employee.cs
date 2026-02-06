using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; } = null!;

      
      [Required(ErrorMessage = "Phone number is required")]
[RegularExpression(@"^[0-9]{5,15}$",
    ErrorMessage = "Phone must be between 5 and 15 digits")]
public string Phone { get; set; } = null!;

    }
}
