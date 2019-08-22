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
            // TODO: Gather user accounts test data here.
            // TODO: Asynchronously persist the user accounts test data into the persistence media (e.g. database).
            // TODO: Temporarily using username const below. Remove it after this is gathered from the persistence media.
            // ReSharper disable once StringLiteralTypo
            const string username = "lesair";
            const string password = "123@321";
            var userAccount1 = new ei_core.Entities.UserAccountAggregate.UserAccount(0, DateTime.Today, username, password);
            await InsertAsync(userAccount1);

            var query = new UserAccountQuery { Username = username };
        }
    }
}
