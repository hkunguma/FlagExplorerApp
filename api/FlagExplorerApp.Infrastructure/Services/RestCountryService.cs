using FlagExplorerApp.Application.Contracts.Interfaces;
using FlagExplorerApp.Application.Models;
using FlagExplorerApp.Common.Contracts;
using FlagExplorerApp.Common.Helpers;
using FlagExplorerApp.Infrastructure.Configuration;
using FlagExplorerApp.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Infrastructure.Services
{
    public class RestCountryService : IRestCountryService
    {
        private readonly RestCountryConfig _restCountryConfig;

        public RestCountryService(IOptions<RestCountryConfig> restCountryConfigOptions)
        {
            _restCountryConfig = restCountryConfigOptions.Value;
        }

        public async Task<IList<Country>> GetAllCountries()
        {
            IHttpClient<IEnumerable<CountryData>> _httpClient = new HttpClientHelper<IEnumerable<CountryData>>(_restCountryConfig.RestCountryBaseUrl);
            var result = await _httpClient.GetAsync(_restCountryConfig.GetAllEndpoint);

            IList<Country> countries = new List<Country>();

            if (result != null)
            {
                foreach (CountryData data in result)
                {
                    //data.Flags?.TryGetValue("png", out flag);
                    countries.Add(new Country
                    {
                        Name = data.Name?.Common,
                        Flag = data.Flags?.GetValueOrDefault<string, string>("png")
                    });
                }
            }

            return countries;
        }

        public async Task<CountryDetail> GetCountryByName(string name)
        {
            IHttpClient<IEnumerable<CountryData>> _httpClient = new HttpClientHelper<IEnumerable<CountryData>>(_restCountryConfig.RestCountryBaseUrl);
            
            StringBuilder requestUri = new StringBuilder(_restCountryConfig.GetByNameEndpoint);
            requestUri.Append($"/{name}");
            var result = await _httpClient.GetAsync(requestUri.ToString());
            
            CountryData? data = result?.FirstOrDefault();

            if (data != null) {
                return new CountryDetail
                {
                    Name = data.Name.Common,
                    Flag = data.Flags?.GetValueOrDefault<string, string>("png"), //data.Flags.TryGetValue("png", out string flag) ? flag : string.Empty,
                    Population = data.Population,
                    Capital = data.Capital?.FirstOrDefault()
                };
            }

            return default(CountryDetail);
        }
    }
}
