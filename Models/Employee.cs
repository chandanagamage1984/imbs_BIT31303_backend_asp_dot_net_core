namespace EmployeesManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // Navigation property
        public Department Department { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }

    // DTO for employee with department details (READ ONLY)
    public class EmployeeWithDepartmentDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
    }

    // DTO for employee input (without department data)
    public class EmployeeInputDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}