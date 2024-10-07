namespace ASPTableEditor.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsMarried { get; set; }
        public string Phone { get; set; }
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
