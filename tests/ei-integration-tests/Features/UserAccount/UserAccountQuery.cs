using System.Collections.Generic;
using ei_infrastructure.Data.Queries;
using Shouldly;
using Xunit;
using static ei_integration_tests.SliceFixture;
// ReSharper disable StringLiteralTypo

namespace ei_integration_tests.Features.UserAccount
{
    public class UserAccountQuery : IntegrationTestBase
    {
        [Fact]
        public async void ReturnsAUserAccountBasedOnAUsername()
        {
            const string testedUsername = "limon";
            var userAccount1 = new ei_infrastructure.Data.POCOs.UserAccount
                { Username = "loerardo", Password = "123@321"};
            var userAccount2 = new ei_infrastructure.Data.POCOs.UserAccount
                { Username = testedUsername, Password = "p4ssw0rd" };
            var userAccount3 = new ei_infrastructure.Data.POCOs.UserAccount
                { Username = "mota", Password = "_+&(=@|!" };
            var userAccounts = new List<ei_infrastructure.Data.POCOs.UserAccount>
                {userAccount1, userAccount2, userAccount3};
            await InsertAsync(userAccounts);

            var query = new ei_infrastructure.Data.Queries.UserAccountQuery { Username = testedUsername };
            var result = await SendAsync(query);

            result.ShouldNotBeNull();
            result.Username.ShouldBe(testedUsername);
            result.Id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async void ReturnsNullWhenAUserAccountCouldNotBeFoundByUsername()
        {
            const string testedUsername = "joseph";
            var userAccount = new ei_infrastructure.Data.POCOs.UserAccount
                { Username = "lesair", Password = "123@321" };
            await InsertAsync(userAccount);

            var query = new ei_infrastructure.Data.Queries.UserAccountQuery { Username = testedUsername };
            var result = await SendAsync(query);

            result.ShouldBeNull();
        }
    }
}
