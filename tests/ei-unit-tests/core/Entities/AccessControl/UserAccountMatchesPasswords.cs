using System;
using ei_core.Entities.AccessControl;
using Xunit;

namespace ei_unit_tests.core.Entities.AccessControl
{
    public class UserAccountMatchesPasswords
    {
        private const int TestId = 28;
        private readonly DateTime _testCreationDate = new DateTime(1980, 1, 1);
        private const string TestUsername = "jsmith";
        private const string TestPassword = "p4ssw0rd";

        [Fact]
        public void PasswordsMatch()
        {
            const bool expected = true;
            const string passwordToCompare = "p4ssw0rd";
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            var actual = userAccount.PasswordMatches(passwordToCompare);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PasswordsDoNotMatchDifferentStrings()
        {
            const bool expected = false;
            const string passwordToValidate = "dummyPass";
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            var actual = userAccount.PasswordMatches(passwordToValidate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PasswordsDoNotMatchDifferentCase()
        {
            const bool expected = false;
            const string passwordToValidate = "P4ssw0rd";
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            var actual = userAccount.PasswordMatches(passwordToValidate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PasswordsDoNotMatchNullString()
        {
            const string passwordToValidate = null;
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            Assert.Throws<ArgumentNullException>(() => userAccount.PasswordMatches(passwordToValidate));
        }

        [Fact]
        public void PasswordsDoNotMatchEmptyString()
        {
            const string passwordToValidate = "";
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            Assert.Throws<ArgumentException>(() => userAccount.PasswordMatches(passwordToValidate));
        }

        [Fact]
        public void PasswordsDoNotMatchLeadingAndTrailingSpaces()
        {
            const bool expected = false;
            const string passwordToValidate = " p4ssw0rd ";
            var userAccount = new UserAccountAggregate(TestId, _testCreationDate, TestUsername, TestPassword);
            var actual = userAccount.PasswordMatches(passwordToValidate);
            Assert.Equal(expected, actual);
        }
    }
}
