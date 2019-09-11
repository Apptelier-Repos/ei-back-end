using System.Collections.Generic;
using ei_infrastructure.Data.Queries;
using Shouldly;
using Xunit;
using static ei_integration_tests.SliceFixture;

// ReSharper disable StringLiteralTypo

namespace ei_integration_tests.Features.UserAccount
{
    public class GetAUserAccountByUsernameTest : IntegrationTestBase
    {
        [Fact]
        public async void ReturnsAUserAccountWhenThereIsAMatch()
        {
            const string matchedUsername = "limon";
            var userAccounts = new List<ei_infrastructure.Data.POCOs.UserAccount>
            {
                new ei_infrastructure.Data.POCOs.UserAccount {Username = "rios", Password = "loerardo@321"},
                new ei_infrastructure.Data.POCOs.UserAccount {Username = matchedUsername, Password = "p4ssw0rd"},
                new ei_infrastructure.Data.POCOs.UserAccount {Username = "mota", Password = "_+&(=@|!"}
            };
            await InsertAsync(userAccounts);

            var query = new GetAUserAccountByUsername.Query(matchedUsername);
            var result = await SendAsync(query);

            result.ShouldNotBeNull();
            result.Username.ShouldBe(matchedUsername);
            result.Id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async void ReturnsNullWhenTheAreNoMatches()
        {
            const string unmatchedUsername = "joseph";
            var userAccount = new ei_infrastructure.Data.POCOs.UserAccount
                {Username = "lesair", Password = "123@321"};
            await InsertAsync(userAccount);

            var query = new GetAUserAccountByUsername.Query(unmatchedUsername);
            var result = await SendAsync(query);

            result.ShouldBeNull();
        }
    }
}