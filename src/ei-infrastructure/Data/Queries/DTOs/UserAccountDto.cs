using System;

namespace ei_infrastructure.Data.Queries.DTOs
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}