using FlagExplorerApp.Application.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Application.Contracts.Interfaces
{
    public interface ICountryService
    {
        Task<IList<Country>> GetAllCountries();
        Task<CountryDetail> GetCountryByName(string name);
    }
}
