using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Infrastructure.Models
{
    internal class CountryData
    {
        public NameData Name { get; set; }
        public Dictionary<string, string> Flags { get; set; }
        public List<string> Capital { get; set; }
        public int Population { get; set; }
    }
}
