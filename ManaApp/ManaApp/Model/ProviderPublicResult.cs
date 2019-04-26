using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.Model
{
    public class ProviderPublicResult
    {
        public ProviderAppointment[] provider_appointments { get; set; }
        public ProviderInfo provider_info { get; set; }
    }
}
