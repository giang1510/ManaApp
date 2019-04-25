using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.Model
{
    public class ProviderInfo
    {
        public string provider_name { get; set; }
        public string provider_address { get; set; }
        public string provider_email { get; set; }
        public string provider_phone_number { get; set; }
        public int slot_range_in_min { get; set; }
        public int days_till_next_pos_app { get; set; }
        public int pos_app_range_in_days { get; set; }
        public int start_work_time { get; set; }
        public int end_work_time { get; set; }
    }
}
