using ei_infrastructure.Data.Queries;
using Shouldly;
using Xunit;
using static ei_slice.Fixture;
using static ei_utils.StringUtils;

// ReSharper disable StringLiteralTypo

namespace ei_integration_tests.Features.WireColor
{
    public class GetAWireColorByCodeTest : IntegrationTestBase
    {
        [Fact]
        public async void ReturnsAWireColorWithCustomBaseColorAndNoStripeColor()
        {
            const string customCode = "X3";
            const string customName = "Custom color 3";
            const string customTranslatedName = "Color personalizado 3";
            const string customColorHexTriplet = "#ABCDEF";
            const string customColorName = "ffabcdef";
            var wireColors = new[]
            {
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = customCode, Name = customName, TranslatedName = customTranslatedName,
                    BaseColor = customColorHexTriplet
                }
            };
            await InsertAsync(wireColors);

            var query = new GetAWireColorByCode.Query(customCode);
            var result = await SendAsync(query);

            result.ShouldNotBeNull();
            result.Id.ShouldBeGreaterThan(0);
            result.Code.ShouldBe(customCode);
            result.Name.ShouldBe(customName);
            result.TranslatedName.ShouldBe(customTranslatedName);
            result.BaseColor.Name.ShouldBe(customColorName);
            result.BaseColor.IsKnownColor.ShouldBeFalse();
            result.StripeColor.HasValue.ShouldBeFalse();
        }

        [Fact]
        public async void ReturnsAWireColorWithKnownBaseAndStripeColors()
        {
            const string lavenderOrangeCode = "LA-O";
            const string lavenderOrangeName = "Lavender-Orange";
            const string lavenderOrangeTranslatedName = "Lavanda-Naranja";
            const string lavenderColorHexTriplet = "#E6E6FA";
            const string lavenderColorName = "Lavender";
            const string orangeColorHexTriplet = "#FFA500";
            const string orangeColorName = "Orange";
            var wireColors = new[]
            {
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "B", Name = "Black", TranslatedName = "Negro", BaseColor = "#000000"
                },
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "O", Name = "Orange", TranslatedName = "Naranja",
                    BaseColor = orangeColorHexTriplet
                },
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = lavenderOrangeCode, Name = lavenderOrangeName, TranslatedName = lavenderOrangeTranslatedName,
                    BaseColor = lavenderColorHexTriplet,
                    StripeColor = orangeColorHexTriplet
                }
            };
            await InsertAsync(wireColors);

            var query = new GetAWireColorByCode.Query(lavenderOrangeCode);
            var result = await SendAsync(query);

            result.ShouldNotBeNull();
            result.Id.ShouldBeGreaterThan(0);
            result.Code.ShouldBe(lavenderOrangeCode);
            result.Name.ShouldBe(lavenderOrangeName);
            result.TranslatedName.ShouldBe(lavenderOrangeTranslatedName);
            result.BaseColor.Name.ShouldBe(lavenderColorName);
            result.BaseColor.IsKnownColor.ShouldBeTrue();
            result.StripeColor.HasValue.ShouldBeTrue();
            result.StripeColor?.Name.ShouldBe(orangeColorName);
            result.StripeColor?.IsKnownColor.ShouldBeTrue();
        }

        [Fact]
        public async void ReturnsNullWhenThereAreNoMatches()
        {
            var randomCode = RandomString(5);
            var wireColor = new ei_infrastructure.Data.POCOs.WireColor
            {
                Code = "LA-P",
                Name = "Lavender-Pink",
                TranslatedName = "Lavanda-Rosa",
                BaseColor = "#E6E6FA",
                StripeColor = "#FFC0CB"
            };
            await InsertAsync(wireColor);

            var query = new GetAWireColorByCode.Query(randomCode);
            var result = await SendAsync(query);

            result.ShouldBeNull();
        }
    }
}