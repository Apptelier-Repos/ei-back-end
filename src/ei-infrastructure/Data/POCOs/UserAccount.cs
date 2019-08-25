using System;
using Dapper.Contrib.Extensions;

namespace ei_infrastructure.Data.POCOs
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Computed]
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}