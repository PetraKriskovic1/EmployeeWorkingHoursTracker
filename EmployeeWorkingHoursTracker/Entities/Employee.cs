using System;
using System.Linq;
//using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkingHoursTracker.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } =  string.Empty;
        public string CreatedDate { get; set; } = DateTime.Now.ToLocalTime().ToString();
        public string UpdatedAt { get; set; } = DateTime.Now.ToLocalTime().ToString();
        public List<TimeTracking> TimeTrackings { get; set; }
    }
}
