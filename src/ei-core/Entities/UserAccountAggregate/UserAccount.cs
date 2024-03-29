﻿using System;
using Ardalis.GuardClauses;

namespace ei_core.Entities.UserAccountAggregate
{
    public sealed class UserAccount : BaseEntity
    {
        public UserAccount(int id, DateTime creationDate, string username, string password) : base(id)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            CreationDate = creationDate;
            Username = username;
            Password = password;
        }

        public DateTime CreationDate { get; }
        public string Username { get; }
        public string Password { get; }

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