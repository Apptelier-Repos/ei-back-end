using System;

namespace ei_infrastructure.Data.Queries.DTOs
{
    public class UserAccount : ei_core.Interfaces.IUserAccountDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}