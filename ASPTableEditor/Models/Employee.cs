using System.ComponentModel.DataAnnotations;

namespace ASPTableEditor.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }
        public bool IsMarried { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public int Salary { get; set; }
        public Employee()
        {
            Id = 0;
            Name = string.Empty;
            BirthDate = DateTime.MinValue;
            IsMarried = false;
            Phone = string.Empty;
            Salary = 0;
        }
    }
}
