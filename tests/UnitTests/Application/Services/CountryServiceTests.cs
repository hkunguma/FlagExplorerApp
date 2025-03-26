using FlagExplorerApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestUtils;

namespace UnitTests.Application.Services
{
    [Collection("RestCountryCollection")]
    public class CountryServiceTests
    {
        private readonly RestCountryFixture _restCountryFixture;

        public CountryServiceTests(RestCountryFixture restCountryFixture) 
        {
            _restCountryFixture = restCountryFixture;
        }

        [Fact]
        public async Task Given_ExternalApiHasData_When_GetAllCountries_Then_ReturnCountryList()
        {
            // Arrange
            var countryService = new CountryService(_restCountryFixture.MockRestCountryService.Object);
            string expectedName = "Hungary";
            string expectedFlag = "https://flagcdn.com/w320/hu.png";

            // Act
            var result = await countryService.GetAllCountries();

            var countryObject = result.First(x => x.Name.Equals("Hungary", StringComparison.OrdinalIgnoreCase));

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(countryObject);
            Assert.Equal(expectedName, countryObject.Name);
            Assert.Equal(expectedFlag, countryObject.Flag);
        }
    }
}
