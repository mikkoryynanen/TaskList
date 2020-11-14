using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.API.Models;

namespace TaskList.API.Services
{
    public interface IUserRepository
    {
        User CreateUser();
        User FindUser(Guid userId);
        IEnumerable<User> GetAllUsers();
        bool UserExists(Guid userId);
    }
}
