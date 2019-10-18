using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ei_infrastructure.Data.POCOs;
using ei_web_api.ViewModels;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using static ei_slice.Fixture;

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
        public async Task ReturnsACustomWireColor()
        {
            const string colorCode = "X1";
            const string colorName = "Custom color 1";
            const string colorTranslatedName = "Color personalizado 1";
            const string baseColorHexTriplet = "#ABCDEF";
            const string baseColorKnownName = baseColorHexTriplet;

            var wireColors = new[]
            {
                new WireColor
                {
                    Code = colorCode, Name = colorName, TranslatedName = colorTranslatedName,
                    BaseColor = baseColorHexTriplet
                }
            };
            await InsertAsync(wireColors);


            var response = await Client.GetAsync($"api/wirecolor/{colorCode}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WireColorViewModel>(stringResponse);

            model.ShouldNotBeNull();
            model.Id.ShouldBeGreaterThan(0);
            model.Code.ShouldBe(colorCode);
            model.Name.ShouldBe(colorName);
            model.TranslatedName.ShouldBe(colorTranslatedName);
            model.BaseWebColor.ShouldBe(baseColorKnownName);
            model.StripeWebColor.ShouldBeNull();
        }

        [Fact]
        public async Task ReturnsAKnownWireColor()
        {
            const string colorCode = "LA-SB";
            const string colorName = "Lavender-Sky Blue";
            const string colorTranslatedName = "Lavanda-Azul Cielo";
            const string baseColorHexTriplet = "#E6E6FA";
            const string baseColorKnownName = "Lavender";
            const string stripeColorHexTriplet = "#87CEEB";
            const string stripeColorKnownName = "SkyBlue";

            var wireColors = new[]
            {
                new WireColor
                {
                    Code = colorCode, Name = colorName, TranslatedName = colorTranslatedName,
                    BaseColor = baseColorHexTriplet, StripeColor = stripeColorHexTriplet
                }
            };
            await InsertAsync(wireColors);


            var response = await Client.GetAsync($"api/wirecolor/{colorCode}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WireColorViewModel>(stringResponse);

            model.ShouldNotBeNull();
            model.Id.ShouldBeGreaterThan(0);
            model.Code.ShouldBe(colorCode);
            model.Name.ShouldBe(colorName);
            model.TranslatedName.ShouldBe(colorTranslatedName);
            model.BaseWebColor.ShouldBe(baseColorKnownName);
            model.StripeWebColor.ShouldBe(stripeColorKnownName);
        }

        [Fact]
        public async Task ReturnsNoContent()
        {
            var colorCode = "A6K9TFBM1";

            var response = await Client.GetAsync($"api/wirecolor/{colorCode}");
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}