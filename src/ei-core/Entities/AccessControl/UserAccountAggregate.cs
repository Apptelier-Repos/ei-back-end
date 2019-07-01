using System;
using Ardalis.GuardClauses;

namespace ei_core.Entities.AccessControl
{
    public sealed class UserAccountAggregate : BaseEntity
    {
        public DateTime CreationDate { get; }
        public string Username { get; }
        public string Password { get; }

        public UserAccountAggregate(int id, DateTime creationDate, string username, string password) : base(id)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            CreationDate = creationDate;
            Username = username;
            Password = password;
        }

        public bool PasswordMatches(string passwordToCompare)
        {
            Guard.Against.NullOrWhiteSpace(passwordToCompare, nameof(passwordToCompare));

            return Password.Equals(passwordToCompare, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"({Id}) {Username}";
        }
    }
}