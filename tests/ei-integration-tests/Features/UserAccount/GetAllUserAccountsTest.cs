using System.Linq;
using ei_infrastructure.Data.Queries;
using FakeItEasy;
using FakeItEasy.Creation;
using Shouldly;
using Xunit;
using static ei_integration_tests.SliceFixture;
using static ei_infrastructure.Utils.StringUtils;

namespace ei_integration_tests.Features.UserAccount
{
    public class GetAllUserAccountsTest : IntegrationTestBase
    {
        private void UserAccountOptionsBuilder(IFakeOptions<ei_infrastructure.Data.POCOs.UserAccount> fakeOptions)
        {
            fakeOptions.ConfigureFake(userAccount =>
            {
                userAccount.Username = RandomString(10);
                userAccount.Password = RandomString(6);
            });
        }

        [Fact]
        public async void ReturnsAllExistingUserAccounts()
        {
            const int numberOfFakeAccounts = 10;
            var fakeAccounts =
                A.CollectionOfFake<ei_infrastructure.Data.POCOs.UserAccount>(numberOfFakeAccounts,
                    UserAccountOptionsBuilder);
            await InsertAsync(fakeAccounts);

            var query = new GetAllUserAccounts.Query();
            var result = (await SendAsync(query)).ToList();

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBeGreaterThanOrEqualTo(numberOfFakeAccounts);
        }
    }
}