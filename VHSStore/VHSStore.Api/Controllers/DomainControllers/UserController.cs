using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.UserModels;
using VHSStore.Utility;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserController(IUserRepository userRepository, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userRepository = userRepository;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userRepository.GetAllAsync();

            return Ok(new BaseResponse<IEnumerable<User>>()
            {
                Body = data,
            });
        }

        [HttpGet("Login")]
        public async Task<IActionResult> GetByUser(string userName, string password)
        {
            var data = await _userRepository.GetByUserNameAsync(userName);

            if (data == null)
            {
                return Ok(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = "User does not exist."
                });
            }

            var passwordHasher = new StringHasher(password, data.Salt);

            if (passwordHasher.HashedString == data.Password)
            {
                return Ok(new BaseResponse<string>()
                { 
                    Body = _jwtAuthenticationManager.Authenticate(),
                    HasError = false, 
                });
            }

            return Ok(new BaseResponse<string>()
            {
                HasError = true,
                Error = "Username or password are incorrect."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUser addUser)
        {
            var checkUserAlreadyExists = await _userRepository.GetByUserNameAsync(addUser.UserName);

            if (!(checkUserAlreadyExists == null))
            {
                return Ok(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = "User already exists."
                });
            }

            var passwordHasher = new StringHasher(addUser.Password);

            var user = new User(addUser, passwordHasher.HashedString, Convert.ToBase64String(passwordHasher.Salt));

            var data = await _userRepository.AddAsync(user);

            return Ok(new BaseResponse<string>()
            { 
                Body = $"Affected rows: {data}"
            });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _userRepository.DeleteAsync(id);

            return Ok(new BaseResponse<string>()
            { 
                Body = $"Affected Rows: {data}"
            });
        }

        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(string userName)
        {
            return Ok("Successfully send email");
        }
    }
}
