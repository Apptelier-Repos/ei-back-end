using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ei_infrastructure.Data.POCOs;
using ei_web_api.ViewModels;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using static ei_slice.Fixture;
using static ei_utils.StringUtils;

// ReSharper disable StringLiteralTypo

namespace ei_functional_tests.Web.Controllers
{
    public class WireColorControllerGetByCode : IClassFixture<CustomWebApplicationFactory>
    {
        public WireColorControllerGetByCode(CustomWebApplicationFactory factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsAWireColorWithHexTripletBaseWebColorAndNoStripeWebColor()
        {
            const string customCode = "X1";
            const string customName = "Custom color 1";
            const string customTranslatedName = "Color personalizado 1";
            const string customColorHexTriplet = "#ABCDEF";
            const string customBaseWebColor = customColorHexTriplet;

            var wireColors = new[]
            {
                new WireColor
                {
                    Code = customCode, Name = customName, TranslatedName = customTranslatedName,
                    BaseColor = customColorHexTriplet
                }
            };
            await InsertAsync(wireColors);


            var response = await Client.GetAsync($"api/wirecolor/{customCode}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WireColorViewModel>(stringResponse);

            model.ShouldNotBeNull();
            model.Id.ShouldBeGreaterThan(0);
            model.Code.ShouldBe(customCode);
            model.Name.ShouldBe(customName);
            model.TranslatedName.ShouldBe(customTranslatedName);
            model.BaseWebColor.ShouldBe(customBaseWebColor);
            model.StripeWebColor.ShouldBeNull();
        }

        [Fact]
        public async Task ReturnsAWireColorWithKnownBaseWebColorAndStripeWebColor()
        {
            const string lavenderSkyBlueCode = "LA-SB";
            const string lavenderSkyBlueName = "Lavender-Sky Blue";
            const string lavenderSkyBlueTranslatedName = "Lavanda-Azul Cielo";
            const string lavenderHexTriplet = "#E6E6FA";
            const string lavenderWebColor = "Lavender";
            const string skyBlueHexTriplet = "#87CEEB";
            const string skyBlueWebColor = "SkyBlue";

            var wireColors = new[]
            {
                new WireColor
                {
                    Code = lavenderSkyBlueCode, Name = lavenderSkyBlueName,
                    TranslatedName = lavenderSkyBlueTranslatedName,
                    BaseColor = lavenderHexTriplet, StripeColor = skyBlueHexTriplet
                }
            };
            await InsertAsync(wireColors);

            var response = await Client.GetAsync($"api/wirecolor/{lavenderSkyBlueCode}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WireColorViewModel>(stringResponse);

            model.ShouldNotBeNull();
            model.Id.ShouldBeGreaterThan(0);
            model.Code.ShouldBe(lavenderSkyBlueCode);
            model.Name.ShouldBe(lavenderSkyBlueName);
            model.TranslatedName.ShouldBe(lavenderSkyBlueTranslatedName);
            model.BaseWebColor.ShouldBe(lavenderWebColor);
            model.StripeWebColor.ShouldBe(skyBlueWebColor);
        }

        [Fact]
        public async Task ReturnsNoContent()
        {
            var randomCode = RandomString(5);

            var response = await Client.GetAsync($"api/wirecolor/{randomCode}");
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}