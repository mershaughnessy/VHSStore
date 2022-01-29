using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllGenres()
        {
            try
            {
                var data = await _unitOfWork.Genres.GetAllAsync();

                return Ok(new BaseResponse<IEnumerable<GenreModel>>()
                { 
                    Body = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGenre(string genreName)
        {
            try
            {
                var data = await _unitOfWork.Genres.AddAsync(new GenreModel(genreName));


                return Ok(new BaseResponse<string>()
                {
                    Body = $"Affected rows: {data}"
                }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var data = await _unitOfWork.Genres.GetByIndexIdAsync(id);

                return Ok(new BaseResponse<GenreModel>()
                { 
                    Body = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                { 
                    HasError = true,
                    Error = ex.Message
                });
            }
        }
    }
}
