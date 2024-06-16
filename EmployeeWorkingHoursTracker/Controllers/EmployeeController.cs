using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeWorkingHoursTracker.Entities;
using EmployeeWorkingHoursTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkingHoursTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }


        // Create Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            employee.CreatedDate = DateTime.Now.ToLocalTime().ToString();
            employee.UpdatedAt = DateTime.Now.ToLocalTime().ToString();
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // Get all Employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _context.employees.ToListAsync();
            return Ok(employees);
        }

        // Get Employee by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);

            if(employee == null)
            { return NotFound("Employee not found");
            }

            return employee; ;
        }

        // Update Employee
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmployee)
        {
            var dbEmployee = await _context.employees.FindAsync(updatedEmployee.Id);

            if (dbEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            dbEmployee.UpdatedAt = DateTime.Now.ToLocalTime().ToString();
            dbEmployee.FirstName = updatedEmployee.FirstName;
            dbEmployee.LastName = updatedEmployee.LastName;
            _context.Entry(dbEmployee).State = EntityState.Modified;

             await _context.SaveChangesAsync();

            return dbEmployee;
            
        }

        // Delete Employee
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.employees.ToListAsync());
        }




    }
}
