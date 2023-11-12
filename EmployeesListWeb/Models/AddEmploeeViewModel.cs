using EmployeesListWeb.Models.Domain;

namespace EmployeesListWeb.Models
{
    public class AddEmploeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
