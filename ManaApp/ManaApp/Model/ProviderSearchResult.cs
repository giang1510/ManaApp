using System;
using System.Collections.Generic;
using System.Text;

namespace ManaApp.Model
{
    public class ProviderSearchResult
    {
        public string search_text { get; set; }
        public Provider[] provider_infos { get; set; }
    }
}
