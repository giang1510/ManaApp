using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.Model
{
    public class ProviderAppointment
    {
        // All datetime data from server is in UTC format (universal time),
        // which is why it must be converted to local time
        private DateTime startDateUTC;
        private DateTime endDateUTC;

        public string appointment_name { get; set; }
        public DateTime start_date
        {
            get
            {
                return startDateUTC.ToLocalTime();
            }
            set
            {
                startDateUTC = value.ToUniversalTime();
            }
        }
        public DateTime end_date
        {
            get
            {
                return endDateUTC.ToLocalTime();
            }
            set
            {
                endDateUTC = value.ToUniversalTime();
            }
        }
        public string color { get; set; }
        public string notification { get; set; }
        public UserInfo user_info { get; set; }
        public string answers { get; set; }
    }
}
