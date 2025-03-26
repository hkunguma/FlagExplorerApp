using FlagExplorerApp.Application.Models;
using FlagExplorerAppApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestUtils;

namespace UnitTests.Api.Controllers
{
    [Collection("RestCountryCollection")]
    public class CountryControllerTests
    {
        private readonly RestCountryFixture _restCountryFixture;

        public CountryControllerTests(RestCountryFixture restCountryFixture) 
        {
            this._restCountryFixture = restCountryFixture;
        }

        [Fact]
        public async Task Given_ExternalApiAccessible_When_GetRequest_Then_ReturnCountryList()
        {
            // Arrange
            var controller = new CountryController(_restCountryFixture.MockCountryService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<List<Country>>(okResult.Value);
            Assert.True(resultValue.Count > 0);
        }
    }
}
