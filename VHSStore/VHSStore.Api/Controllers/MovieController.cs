using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.MovieModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _movieRepository.GetAllAsync();

                return Ok(new BaseResponse<IEnumerable<MovieModel>>()
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

        [HttpGet("ById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var data = await _movieRepository.GetByIdAsync(id);

                return Ok(new BaseResponse<MovieModel>()
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
        public async Task<IActionResult> AddMovie(AddMovieModel newMovie)
        {
            try
            {
                var data = await _movieRepository.AddAsync(new MovieModel(newMovie));

                return Ok(new BaseResponse<string>()
                {
                    Body = $"Affected Rows: {data}"
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

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var data = await _movieRepository.DeleteAsync(id);

                return Ok(new BaseResponse<string>()
                { 
                    Body = $"Affected Rows: {data}"
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
