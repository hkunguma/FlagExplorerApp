using IntegrationTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class FlagExplorerAppWebApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public FlagExplorerAppWebApiTest(WebApplicationFactory<Program> webApplicationFactory)
        {
            this._httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Given_ApiAccessible_When_GetEndpointRequest_Then_ReturnCountryList()
        {
            // Arrange
            var apiEndpoint = "/api/country/countries";
            var countryName = "South Africa";
            var flag = "https://flagcdn.com/w320/za.png";

            // Act
            var response = await _httpClient.GetAsync(apiEndpoint);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Country>>(content);
            Country? country = result?.First(x => x.Name == countryName);

            // Assert            
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(country);
            Assert.Equal(countryName, country.Name);
            Assert.Equal(flag, country.Flag);
        }
    }
}
