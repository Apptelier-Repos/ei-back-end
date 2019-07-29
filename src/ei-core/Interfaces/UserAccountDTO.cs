using System;

namespace ei_core.Interfaces
{
    public interface IUserAccountDto
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}