using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeWarsBackend.Models.DTO;
using CodeWarsBackend.Models;
using CodeWarsBackend.Services;

namespace CodeWarsBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService dataFromService){
            _data = dataFromService;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDto User){
            return _data.Login(User);
        }

        [HttpPost]
        [Route("AddUser")]
        public bool AddUser(CreateAccountDto UserToAdd){
            return _data.AddUser(UserToAdd);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate){
            return _data.UpdateUser(userToUpdate);
        }
        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public UserModel GetUserByUsername(string? username){
            return _data.GetUserByUsername(username);
        }
    }
}