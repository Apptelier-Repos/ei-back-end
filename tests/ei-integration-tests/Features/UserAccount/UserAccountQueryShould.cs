using System;
using ei_infrastructure.Data.Queries;
using Xunit;
using static ei_integration_tests.SliceFixture;

namespace ei_integration_tests.Features.UserAccount
{
    public class UserAccountQueryShould : IntegrationTestBase
    {
        [Fact]
        public async void ReturnAUserAccountBasedOnAUsername()
        {
            // TODO: Temporarily using username const below. Remove it after this is gathered from the persistence media.
            // ReSharper disable once StringLiteralTypo
            const string username = "lesair";
            const string password = "123@321";
            // TODO: Change DDD entity below for a DTO for direct DB insertion purposes. Consider sharing these DTOs since we're not using EF.
            var userAccount1 = new ei_core.Entities.UserAccountAggregate.UserAccount(999, DateTime.Today, username, password);
            await InsertAsync(userAccount1);

            var demoQuery = new DemoQuery { Number = 5 };
            var demoResult = await SendAsync(demoQuery);

            var query = new UserAccountQuery { Username = username };
            var result = await SendAsync(query);
        }
    }
}
