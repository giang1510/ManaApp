using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.Model
{
    public class ProviderAppointment
    {
        public string appointment_name { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string color { get; set; }
        public string notification { get; set; }
        public UserInfo user_info { get; set; }
        public string answers { get; set; }
    }
}
