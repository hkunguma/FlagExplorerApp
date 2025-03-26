using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Infrastructure.Models
{
    internal class NameData
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public Dictionary<string,object> NativeName { get; set; }
    }
}
