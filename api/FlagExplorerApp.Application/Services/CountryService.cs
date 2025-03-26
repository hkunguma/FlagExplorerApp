using FlagExplorerApp.Application.Contracts.Interfaces;
using FlagExplorerApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRestCountryService restCountryService;

        public CountryService(IRestCountryService restCountryService)
        {
            this.restCountryService = restCountryService;
        }

        public async Task<IList<Country>> GetAllCountries()
        {
            return await restCountryService.GetAllCountries();
        }

        public async Task<CountryDetail> GetCountryByName(string name)
        {
            return await restCountryService.GetCountryByName(name);
        }
    }
}
