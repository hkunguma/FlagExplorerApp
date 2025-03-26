using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Infrastructure.Configuration
{
    public class RestCountryConfig
    {
        public string RestCountryBaseUrl { get; set; }
        public string GetAllEndpoint { get; set; }
        public string GetByNameEndpoint { get; set; }
    }
}
