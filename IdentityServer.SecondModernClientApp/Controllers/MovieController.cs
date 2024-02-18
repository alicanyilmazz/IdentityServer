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
        private readonly IImageServerSaveManager _imageManager;
        private readonly IImageServerSaveService _imageService;
        private readonly IImageServerReadService _imageReadService;

        public MovieController(IService<Movie, MovieDto> movieService, IImageServerSaveManager imageManager, IImageServerSaveService imageService, IImageServerReadService imageReadService)
        {
            _movieService = movieService;
            _imageManager = imageManager;
            _imageService = imageService;
            _imageReadService = imageReadService;
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

        //[Authorize]
        [HttpPost]
        [RequestSizeLimit(Magnitude.ThreeMegabytes)]
        public async Task<IActionResult> Save(IFormFile[] photos, CancellationToken cancellationToken)
        {
            if (photos.Length > Magnitude.ThreeMegabytes)
            {
                return ActionResultInstance(Response<NoDataDto>.Fail($"Image size can not be greater than 3 MB.", 400, true));
            }
            var result = await _imageManager.Save(_imageService, photos.Select(x => new ImageDbServiceRequest
            {
                Name = x.FileName,
                Type = x.ContentType,
                Content = x.OpenReadStream()
            }), Directory.GetCurrentDirectory());
            return ActionResultInstance(result);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _imageReadService.GetPhotosAsync();
            return ActionResultInstance(result);
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string imageId)
        {
            var result = await _imageReadService.GetPhotoAsync(imageId);
            return ActionResultInstance(result);
        }
    }
}
