using ei_infrastructure.Data.Queries;
using Shouldly;
using Xunit;
using static ei_integration_tests.SliceFixture;

// ReSharper disable StringLiteralTypo

namespace ei_integration_tests.Features.WireColor
{
    public class GetAWireColorByCodeTest : IntegrationTestBase
    {
        [Fact]
        public async void ReturnsAWireColorWhenThereIsAMatch()
        {
            const string orangeCode = "O";
            const string orangeName = "Orange";
            const string orangeTranslatedName = "Naranja";
            const string orangeColorKnownName = "Orange";
            const string orangeColorHexTriplet = "#FFA500";
            var wireColors = new[]
            {
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "B", Name = "Black", TranslatedName = "Negro", BaseColor = "#000000"
                },
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = orangeCode, Name = orangeName, TranslatedName = orangeTranslatedName,
                    BaseColor = orangeColorHexTriplet
                },
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "LA-O", Name = "Lavender-Orange", TranslatedName = "Lavanda-Naranja", BaseColor = "#E6E6FA",
                    StripeColor = orangeColorHexTriplet
                }
            };
            await InsertAsync(wireColors);

            var query = new GetAWireColorByCode.Query(orangeCode);
            var result = await SendAsync(query);

            result.ShouldNotBeNull();
            result.Id.ShouldBeGreaterThan(0);
            result.Code.ShouldBe(orangeCode);
            result.Name.ShouldBe(orangeName);
            result.TranslatedName.ShouldBe(orangeTranslatedName);
            result.BaseColor.Name.ShouldBe(orangeColorKnownName);
        }

        [Fact]
        public async void ReturnsNullWhenThereAreNoMatches()
        {
            const string pinkCode = "P";
            var wireColor = new ei_infrastructure.Data.POCOs.WireColor
            {
                Code = "LA-P",
                Name = "Lavender-Pink",
                TranslatedName = "Lavanda-Rosa",
                BaseColor = "#E6E6FA",
                StripeColor = "#FFC0CB"
            };
            await InsertAsync(wireColor);

            var query = new GetAWireColorByCode.Query(pinkCode);
            var result = await SendAsync(query);

            result.ShouldBeNull();
        }
    }
}