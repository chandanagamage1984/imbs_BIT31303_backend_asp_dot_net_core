namespace EmployeesManagementSystem.Models
{
    public class EmployeeProject
    {
        // Foreign keys
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }

    // DTO for EmployeeProject with all related data (READ ONLY)
    public class EmployeeProjectDetailDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    // DTO for EmployeeProject input (only EmployeeId and ProjectId)
    public class EmployeeProjectInputDto
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}