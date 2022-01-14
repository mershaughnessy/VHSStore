using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSStore.Api.Utility;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.UserModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserController(IUnitOfWork unitOfWork, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _unitOfWork = unitOfWork;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _unitOfWork.Users.GetAllAsync();

                return Ok(new GetAllUsersResponse()
                {
                    Body = data,
                    HasError = false,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new GetAllUsersResponse()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Login")]
        public async Task<IActionResult> GetByUser(string userName, string password)
        {
            try
            {
                var data = await _unitOfWork.Users.GetByUserNameAsync(userName);

                if (data == null)
                {
                    return Ok(new LoginUserResponse()
                    { 
                        HasError = true,
                        Error = "User does not exist."
                    });
                }

                var passwordHasher = new StringHasher(password, data.Salt);

                if (passwordHasher.HashedString == data.Password)
                {
                    return Ok(new LoginUserResponse()
                    { 
                        Body = _jwtAuthenticationManager.Authenticate(),
                        HasError = false, 
                    });
                }

                return Ok(new LoginUserResponse()
                {
                    HasError = true,
                    Error = "Username or password are incorrect."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new LoginUserResponse()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUser addUser)
        {
            try
            {
                var checkUserAlreadyExists = await _unitOfWork.Users.GetByUserNameAsync(addUser.UserName);

                if (!(checkUserAlreadyExists == null))
                {
                    return Ok(new AddUserResponse()
                    { 
                        HasError = true,
                        Error = "User already exists."
                    });
                }

                var passwordHasher = new StringHasher(addUser.Password);

                var user = new User() 
                {
                    UserName = addUser.UserName,
                    Password = passwordHasher.HashedString,
                    Salt = Convert.ToBase64String(passwordHasher.Salt),
                    Email = addUser.Email,
                };

                await _unitOfWork.Users.AddAsync(user);

                return Ok(new AddUserResponse()
                { 
                    HasError = false,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AddUserResponse()
                { 
                    HasError = true, 
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.Users.DeleteAsync(id);

                return Ok(new DeleteUserResponse()
                { 
                    HasError = false,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new DeleteUserResponse()
                {
                    HasError = true,
                    Error = ex.Message
                });
            }
        }
    }
}
