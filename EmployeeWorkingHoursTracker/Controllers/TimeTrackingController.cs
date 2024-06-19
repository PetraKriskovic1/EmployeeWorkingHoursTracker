using EmployeeWorkingHoursTracker.Data;
using EmployeeWorkingHoursTracker.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace EmployeeWorkingHoursTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackingController : ControllerBase
    {
        private readonly DataContext _context;

        public TimeTrackingController(DataContext context)
        {
            _context = context;
        }

        // Start Time Tracking
        [HttpPost("start/{employeeId}")]
        public async Task<ActionResult<TimeTracking>> StartTracking(int employeeId)
        {
            var tracking = new TimeTracking
            {
                EmployeeId = employeeId,
                StartTime = DateTime.Now.ToLocalTime().ToString(),
                StartTimeAsDateTime = DateTime.Now.ToLocalTime()
            };

            _context.timeTrackings.Add(tracking);
            await _context.SaveChangesAsync();

            return Ok(tracking);
        }

        // End Time Tracking
        [HttpPost("end/{employeeId}")]
        public async Task<IActionResult> EndTracking(int employeeId)
        {
            var tracking = await _context.timeTrackings
                .Where(t => t.EmployeeId == employeeId && t.EndTime == null)
                .FirstOrDefaultAsync();

            if (tracking == null)
            {
                return NotFound();
            }

            tracking.EndTime = DateTime.Now.ToLocalTime().ToString();
            tracking.EndTimeAsDateTime = DateTime.Now.ToLocalTime();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Get Working Hours for Single Employee
        [HttpGet("report/{employeeId}")]
        public async Task<ActionResult<IEnumerable<TimeTracking>>> GetEmployeeWorkingHours(int employeeId, [FromQuery] string start, [FromQuery] string end)
        {
            DateTime fromStartTime = DateTime.ParseExact(start, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime toEndTime = DateTime.ParseExact(end, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var trackings = await _context.timeTrackings
                .Where(t => t.EmployeeId == employeeId && t.StartTimeAsDateTime >= fromStartTime && t.EndTimeAsDateTime <= toEndTime)
                .ToListAsync();

            if (trackings == null || !trackings.Any())
            {
                return NotFound("No tracking records found for the specified employee and time range.");
            }

            double totalHoursWorked = trackings
                .Sum(t => (t.EndTimeAsDateTime - t.StartTimeAsDateTime)?.TotalHours ?? 0);

            return Ok(totalHoursWorked);
        }

        // Get Working Hours for All Employees
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<TimeTracking>>> GetAllEmployeeWorkingHours([FromQuery] string start, [FromQuery] string end)
        {
            DateTime fromStartTime = DateTime.ParseExact(start, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime toEndTime = DateTime.ParseExact(end, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var trackings = await _context.timeTrackings
                .Where(t => t.StartTimeAsDateTime >= fromStartTime && t.EndTimeAsDateTime <= toEndTime)
                .ToListAsync();

            var totalHoursByEmployee = trackings
        .GroupBy(t => t.EmployeeId)
        .Select(g => new
        {
            EmployeeId = g.Key,
            TotalHoursWorked = g.Sum(t => (t.EndTimeAsDateTime - t.StartTimeAsDateTime)?.TotalHours ?? 0)
        })
        .OrderByDescending(e => e.TotalHoursWorked)
        .ToList();

            return Ok(totalHoursByEmployee);
        }

    }
}
