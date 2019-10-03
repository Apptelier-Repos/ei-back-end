using System.Linq;
using ei_infrastructure.Data.Queries;
using Shouldly;
using Xunit;
using static ei_integration_tests.SliceFixture;

namespace ei_integration_tests.Features.WireColor
{
    public class GetAllWireColorsTest : IntegrationTestBase
    {
        [Fact]
        public async void ReturnsAllExistingWireColors()
        {
            var wireColors = new[]
            {
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "L", Name = "Blue", TranslatedName = "Azul", BaseColor = "#0000FF"
                },
                new ei_infrastructure.Data.POCOs.WireColor
                {
                    Code = "LA-L", Name = "Lavender-Blue", TranslatedName = "Lavanda-Azul", BaseColor = "#0000FF",
                    StripeColor = "#B57EDC"
                }
            };

            await InsertAsync(wireColors);

            var query = new GetAllWireColors.Query();
            var result = (await SendAsync(query)).ToList();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBeGreaterThanOrEqualTo(wireColors.Length);
            result.ShouldContain(wireColor => wireColor.Code == wireColors[0].Code);
            result.ShouldContain(wireColor => wireColor.Code == wireColors[1].Code);
        }
    }
}