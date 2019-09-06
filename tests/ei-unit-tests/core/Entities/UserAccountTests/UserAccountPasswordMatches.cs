using System;
using ei_core.Entities.UserAccountAggregate;
using Shouldly;
using Xunit;

namespace ei_unit_tests.Core.Entities.UserAccountTests
{
    public class UserAccountPasswordMatches
    {
        private const int TestId = 28;
        private readonly DateTime _testCreationDate = new DateTime(1980, 1, 1);
        private const string TestUsername = "jsmith";
        private const string TestPassword = "p4ssw0rd";

        [Fact]
        public void PasswordsDoNotMatchDifferentCase()
        {
            const string passwordToCompare = "P4ssw0rd";
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            userAccount.PasswordMatches(passwordToCompare).ShouldBeFalse();
        }

        [Fact]
        public void PasswordsDoNotMatchDifferentStrings()
        {
            const string passwordToCompare = "dummyPass";
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            userAccount.PasswordMatches(passwordToCompare).ShouldBeFalse();
        }

        [Fact]
        public void PasswordsDoNotMatchEmptyString()
        {
            const string passwordToCompare = "";
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            Assert.Throws<ArgumentException>(() => userAccount.PasswordMatches(passwordToCompare));
        }

        [Fact]
        public void PasswordsDoNotMatchLeadingAndTrailingSpaces()
        {
            const string passwordToCompare = " p4ssw0rd ";
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            userAccount.PasswordMatches(passwordToCompare).ShouldBeFalse();
        }

        [Fact]
        public void PasswordsDoNotMatchNullString()
        {
            const string passwordToCompare = null;
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            Assert.Throws<ArgumentNullException>(() => userAccount.PasswordMatches(passwordToCompare));
        }

        [Fact]
        public void PasswordsMatch()
        {
            const string passwordToCompare = "p4ssw0rd";
            var userAccount = new UserAccount(TestId, _testCreationDate, TestUsername, TestPassword);
            userAccount.PasswordMatches(passwordToCompare).ShouldBeTrue();
        }
    }
}