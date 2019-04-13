using System;

namespace ManaApp.Model
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public bool IsAllDay { get; set; }
    }
}
