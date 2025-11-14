using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace histopat_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private IImageStorageService _imageStorageService;
        public ImageController(IImageStorageService imageStorageService) {
            this._imageStorageService = imageStorageService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> saveImage([FromForm] ImageUpload imageUpload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (imageUpload.File == null || imageUpload.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            await using var stream = imageUpload.File.OpenReadStream();

            var imageUrl = await _imageStorageService.SaveImageAsync(stream, imageUpload.CustomFileName);

            return Ok(imageUrl);
        }
    }
}
