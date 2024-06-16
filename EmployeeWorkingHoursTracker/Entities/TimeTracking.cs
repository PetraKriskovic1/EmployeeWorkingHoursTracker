namespace EmployeeWorkingHoursTracker.Entities
{
    public class TimeTracking
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string StartTime { get; set; }
        public DateTime StartTimeAsDateTime { get; set; } = DateTime.Now.ToLocalTime();

        public string? EndTime { get; set; }

        public DateTime? EndTimeAsDateTime { get;set; }  = DateTime.Now.ToLocalTime();
    }
}