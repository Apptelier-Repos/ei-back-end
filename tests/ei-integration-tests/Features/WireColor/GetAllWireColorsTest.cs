using System.Collections.Generic;
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
            var wireColors = new List<ei_infrastructure.Data.POCOs.WireColor>
            {
                new ei_infrastructure.Data.POCOs.WireColor
                    {Code = "L", Name = "Blue", TranslatedName = "Azul", BaseColor = "#0000ff"}
            };

            await InsertAsync(wireColors);

            var query = new GetAllUserAccounts.Query();
            var result = (await SendAsync(query)).ToList();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}