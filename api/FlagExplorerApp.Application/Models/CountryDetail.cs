using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Application.Models
{
    public class CountryDetail : Country
    {
        public int Population {  get; set; }
        public string? Capital {  get; set; }
    }
}
