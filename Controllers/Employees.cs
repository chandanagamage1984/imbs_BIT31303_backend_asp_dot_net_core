using EmployeesManagementSystem.Data;
using EmployeesManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmpMgtSysDbContext _context;
        public EmployeesController(EmpMgtSysDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWithDepartmentDto>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)  // Include Department data for reading
                .Select(e => new EmployeeWithDepartmentDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    DepartmentName = e.Department.Name,
                    Location = e.Department.Location
                })
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWithDepartmentDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)  // Include Department data for reading
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeWithDepartmentDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    DepartmentName = e.Department.Name,
                    Location = e.Department.Location
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeWithDepartmentDto>> PostEmployee(EmployeeInputDto employeeInput)
        {
            // Validate that the Department exists
            var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentId == employeeInput.DepartmentId);
            if (!departmentExists)
            {
                return BadRequest("Invalid DepartmentId");
            }

            var employee = new Employee
            {
                Name = employeeInput.Name,
                Email = employeeInput.Email,
                Phone = employeeInput.Phone,
                Salary = employeeInput.Salary,
                DepartmentId = employeeInput.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Return the created employee with department details
            var createdEmployee = await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.EmployeeId == employee.EmployeeId)
                .Select(e => new EmployeeWithDepartmentDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    DepartmentName = e.Department.Name,
                    Location = e.Department.Location
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, createdEmployee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeInputDto employeeInput)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Validate that the Department exists
            var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentId == employeeInput.DepartmentId);
            if (!departmentExists)
            {
                return BadRequest("Invalid DepartmentId");
            }

            // Update only the allowed properties
            employee.Name = employeeInput.Name;
            employee.Email = employeeInput.Email;
            employee.Phone = employeeInput.Phone;
            employee.Salary = employeeInput.Salary;
            employee.DepartmentId = employeeInput.DepartmentId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}