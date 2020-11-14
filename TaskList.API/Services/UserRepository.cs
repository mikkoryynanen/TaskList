using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.API.Controllers;
using TaskList.API.Models;

namespace TaskList.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public User CreateUser()
        {
            User user = new User
            {
                Id = Guid.NewGuid()
            };
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return user;
        }

        public User FindUser(Guid userId)
        {
            return _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public bool UserExists(Guid userId)
        {
            return FindUser(userId) != null;
        }
    }
}
