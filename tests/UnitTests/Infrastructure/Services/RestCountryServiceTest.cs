using FlagExplorerApp.Application.Models;
using FlagExplorerApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestUtils;

namespace UnitTests.Infrastructure.Services
{
    [Collection("RestCountryCollection")]
    public class RestCountryServiceTest
    {
        private readonly RestCountryFixture _restCountryFixture;

        public RestCountryServiceTest(RestCountryFixture restCountryFixture) 
        {
            this._restCountryFixture = restCountryFixture;
        }

        [Fact]
        public async Task Given_ExternalApiAccessible_When_GetAllCountries_Then_ReturnCountryList()
        {
            // Arrange
            var restCountryService = new RestCountryService(_restCountryFixture.MockRestCountryConfig.Object);

            // Act
            var result = await restCountryService.GetAllCountries();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task Given_ExternalApiAccessible_When_GetCountryByName_Then_ReturnCountryDetails()
        {
            // Arrange
            var restCountryService = new RestCountryService(_restCountryFixture.MockRestCountryConfig.Object);

            string countryName = "South Georgia";

            CountryDetail expectedCountry = new CountryDetail {
                Name = countryName,
                Flag = "https://flagcdn.com/w320/gs.png",
                Population = 4211,
                Capital = "King Edward Point" };

            // Act
            var result = await restCountryService.GetCountryByName(countryName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCountry, result);
        }
    }
}
