using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.API.Models;
using TaskList.API.Services;

namespace TaskList.API.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser()
        {
            var user = _userRepository.CreateUser();
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }

        [HttpGet("{userId::Guid}")]
        public ActionResult<UserDto> GetUser(Guid userId)
        {
            var user = _userRepository.FindUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

#if DEBUG
        [HttpGet("all")]
        public ActionResult<IEnumerable<User>> GetUsersList()
        {
            List<User> users = _userRepository.GetAllUsers().ToList();
            if (users == null && users.Count <= 0)
            {
                return NotFound();
            }

            return Ok(users);
        }
#endif
    }
}
