using EmployeesManagementSystem.Data;
using EmployeesManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectsController : ControllerBase
    {
        private readonly EmpMgtSysDbContext _context;
        public EmployeeProjectsController(EmpMgtSysDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeProjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDetailDto>>> GetEmployeeProjects()
        {
            return await _context.Employee_Projects
                .Include(ep => ep.Employee)
                    .ThenInclude(e => e.Department)
                .Include(ep => ep.Project)
                .Select(ep => new EmployeeProjectDetailDto
                {
                    EmployeeId = ep.EmployeeId,
                    EmployeeName = ep.Employee.Name,
                    Email = ep.Employee.Email,
                    Phone = ep.Employee.Phone,
                    DepartmentName = ep.Employee.Department.Name,
                    Location = ep.Employee.Department.Location,
                    ProjectName = ep.Project.ProjectName,
                    StartDate = ep.Project.StartDate,
                    EndDate = ep.Project.EndDate
                })
                .ToListAsync();
        }

        // GET: api/EmployeeProjects/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDetailDto>>> GetEmployeeProjectsByEmployee(int employeeId)
        {
            var employeeProjects = await _context.Employee_Projects
                .Include(ep => ep.Employee)
                    .ThenInclude(e => e.Department)
                .Include(ep => ep.Project)
                .Where(ep => ep.EmployeeId == employeeId)
                .Select(ep => new EmployeeProjectDetailDto
                {
                    EmployeeId = ep.EmployeeId,
                    EmployeeName = ep.Employee.Name,
                    Email = ep.Employee.Email,
                    Phone = ep.Employee.Phone,
                    DepartmentName = ep.Employee.Department.Name,
                    Location = ep.Employee.Department.Location,
                    ProjectName = ep.Project.ProjectName,
                    StartDate = ep.Project.StartDate,
                    EndDate = ep.Project.EndDate
                })
                .ToListAsync();

            if (!employeeProjects.Any())
            {
                return NotFound();
            }
            return employeeProjects;
        }

        // GET: api/EmployeeProjects/project/3
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDetailDto>>> GetEmployeeProjectsByProject(int projectId)
        {
            var employeeProjects = await _context.Employee_Projects
                .Include(ep => ep.Employee)
                    .ThenInclude(e => e.Department)
                .Include(ep => ep.Project)
                .Where(ep => ep.ProjectId == projectId)
                .Select(ep => new EmployeeProjectDetailDto
                {
                    EmployeeId = ep.EmployeeId,
                    EmployeeName = ep.Employee.Name,
                    Email = ep.Employee.Email,
                    Phone = ep.Employee.Phone,
                    DepartmentName = ep.Employee.Department.Name,
                    Location = ep.Employee.Department.Location,
                    ProjectName = ep.Project.ProjectName,
                    StartDate = ep.Project.StartDate,
                    EndDate = ep.Project.EndDate
                })
                .ToListAsync();

            if (!employeeProjects.Any())
            {
                return NotFound();
            }
            return employeeProjects;
        }

        // POST: api/EmployeeProjects
        [HttpPost]
        public async Task<ActionResult<EmployeeProjectDetailDto>> PostEmployeeProject(EmployeeProjectInputDto employeeProjectInput)
        {
            // Validate that Employee exists
            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == employeeProjectInput.EmployeeId);
            if (!employeeExists)
            {
                return BadRequest("Invalid EmployeeId");
            }

            // Validate that Project exists
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == employeeProjectInput.ProjectId);
            if (!projectExists)
            {
                return BadRequest("Invalid ProjectId");
            }

            // Check if the assignment already exists
            var existingAssignment = await _context.Employee_Projects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeProjectInput.EmployeeId &&
                                         ep.ProjectId == employeeProjectInput.ProjectId);
            if (existingAssignment != null)
            {
                return BadRequest("This employee is already assigned to this project.");
            }

            var employeeProject = new EmployeeProject
            {
                EmployeeId = employeeProjectInput.EmployeeId,
                ProjectId = employeeProjectInput.ProjectId
            };

            _context.Employee_Projects.Add(employeeProject);
            await _context.SaveChangesAsync();

            // Return the created assignment with all details
            var createdEmployeeProject = await _context.Employee_Projects
                .Include(ep => ep.Employee)
                    .ThenInclude(e => e.Department)
                .Include(ep => ep.Project)
                .Where(ep => ep.EmployeeId == employeeProjectInput.EmployeeId &&
                            ep.ProjectId == employeeProjectInput.ProjectId)
                .Select(ep => new EmployeeProjectDetailDto
                {
                    EmployeeId = ep.EmployeeId,
                    EmployeeName = ep.Employee.Name,
                    Email = ep.Employee.Email,
                    Phone = ep.Employee.Phone,
                    DepartmentName = ep.Employee.Department.Name,
                    Location = ep.Employee.Department.Location,
                    ProjectName = ep.Project.ProjectName,
                    StartDate = ep.Project.StartDate,
                    EndDate = ep.Project.EndDate
                })
                .FirstOrDefaultAsync();

            // Fixed CreatedAtAction call
            return CreatedAtAction(
                nameof(GetEmployeeProjects),
                new { },
                createdEmployeeProject);
        }

        // DELETE: api/EmployeeProjects
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeProject([FromBody] EmployeeProjectInputDto employeeProjectInput)
        {
            var employeeProject = await _context.Employee_Projects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeProjectInput.EmployeeId &&
                                         ep.ProjectId == employeeProjectInput.ProjectId);
            if (employeeProject == null)
            {
                return NotFound();
            }

            _context.Employee_Projects.Remove(employeeProject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/EmployeeProjects (Update assignment)
        [HttpPut]
        public async Task<IActionResult> PutEmployeeProject([FromBody] UpdateEmployeeProjectDto updateDto)
        {
            // Find the existing assignment
            var existingEmployeeProject = await _context.Employee_Projects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == updateDto.OldEmployeeId &&
                                         ep.ProjectId == updateDto.OldProjectId);
            if (existingEmployeeProject == null)
            {
                return NotFound();
            }

            // Validate new Employee exists
            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == updateDto.NewEmployeeId);
            if (!employeeExists)
            {
                return BadRequest("Invalid EmployeeId in new assignment");
            }

            // Validate new Project exists
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == updateDto.NewProjectId);
            if (!projectExists)
            {
                return BadRequest("Invalid ProjectId in new assignment");
            }

            // Check if the new assignment already exists
            var duplicateAssignment = await _context.Employee_Projects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == updateDto.NewEmployeeId &&
                                         ep.ProjectId == updateDto.NewProjectId);
            if (duplicateAssignment != null)
            {
                return BadRequest("This employee is already assigned to this project.");
            }

            // Remove old assignment and create new one
            _context.Employee_Projects.Remove(existingEmployeeProject);

            var newEmployeeProject = new EmployeeProject
            {
                EmployeeId = updateDto.NewEmployeeId,
                ProjectId = updateDto.NewProjectId
            };
            _context.Employee_Projects.Add(newEmployeeProject);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeProjectExists(int employeeId, int projectId)
        {
            return _context.Employee_Projects.Any(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        }
    }

    // DTO for updating employee project assignment
    public class UpdateEmployeeProjectDto
    {
        public int OldEmployeeId { get; set; }
        public int OldProjectId { get; set; }
        public int NewEmployeeId { get; set; }
        public int NewProjectId { get; set; }
    }
}