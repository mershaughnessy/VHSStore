using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUserRepository _userRepository;

        public TestController(IGenreRepository genreRepository, IUserRepository userRepository)
        {
            _genreRepository = genreRepository;
            _userRepository = userRepository;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test(string genre)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var genres = await _genreRepository.AddAsync(new GenreModel() { GenreName = genre });
                    var users = await _userRepository.GetAllAsync();
                    transaction.Complete();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
