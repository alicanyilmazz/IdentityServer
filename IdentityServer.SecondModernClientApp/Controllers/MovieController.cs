using IdentityServer.API2.Core.Abstract;
using IdentityServer.API2.Core.Dtos;
using IdentityServer.API2.Core.Entities;
using IdentityServer.API2.Core.Services;
using IdentityServer.API2.Core.Utilities.Constants;
using IdentityServer.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : CustomBaseController
    {
        private readonly IService<Movie, MovieDto> _movieService;


        public MovieController(IService<Movie, MovieDto> movieService)
        {
            _movieService = movieService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return ActionResultInstance(await _movieService.GetAllAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieDto movieDto)
        {
            return ActionResultInstance(await _movieService.AddAsync(movieDto));
        }     
    }
}
