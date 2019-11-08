using System;
using System.Data.SqlTypes;
using ei_core.Exceptions;
using ei_infrastructure.Data.Commands;
using Shouldly;
using Xunit;
using static ei_slice.Fixture;

// ReSharper disable StringLiteralTypo

namespace ei_integration_tests.Features.UserAccount
{
    public class CreateUserAccountTest : IntegrationTestBase
    {
        [Fact]
        public async void CreatesANewUserAccount()
        {
            const string username = "maddox";
            const string password = "p4$$word";

            var command = new CreateUserAccount.Command {Username = username, Password = password};
            var userAccountId = await SendAsync(command);

            var userAccount = await FindAsync<ei_infrastructure.Data.POCOs.UserAccount>(userAccountId);

            userAccount.ShouldNotBeNull();
            userAccount.Username.ShouldBe(username);
            userAccount.Password.ShouldBe(password);
            userAccount.CreationDate.ShouldBeInRange(SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value);
        }

        [Fact]
        public async void ThrowsAnExceptionWhenDuplicatingUsernames()
        {
            const string username = "dante";
            const string password1 = "P4ss!#$%";
            const string password2 = "@passw0rd";

            var command1 = new CreateUserAccount.Command {Username = username, Password = password1};
            await SendAsync(command1);
            var command2 = new CreateUserAccount.Command {Username = username, Password = password2};
            Exception ex = await Assert.ThrowsAsync<UsernameAlreadyExistsException>(() => SendAsync(command2));
            ex.Message.ShouldContain(username);
        }

        [Fact]
        public async void ThrowsAnExceptionWhenThePasswordIsNull()
        {
            var username = "artaud";
            const string password = null;

            var command = new CreateUserAccount.Command {Username = username, Password = password};
            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => SendAsync(command));
            ex.Message.ShouldContain(nameof(CreateUserAccount.Command.Password));
        }

        [Fact]
        public async void ThrowsAnExceptionWhenTheUsernameIsEmpty()
        {
            var username = string.Empty;
            const string password = "P4ss!#$%";

            var command = new CreateUserAccount.Command {Username = username, Password = password};
            Exception ex = await Assert.ThrowsAsync<ArgumentException>(() => SendAsync(command));
            ex.Message.ShouldContain(nameof(CreateUserAccount.Command.Username));
        }
    }
}