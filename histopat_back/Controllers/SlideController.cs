using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.Topic;
using Microsoft.AspNetCore.Mvc;

namespace histopat_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlideController : ControllerBase
    {
        private ISlideService _slideService;
        public SlideController(ISlideService slideService)
        {
            this._slideService = slideService;
        }

        // GET: api/Slide/subtopic/5
        [HttpGet("subtopic/{subTopicId}")]
        public async Task<ActionResult> FindAllSlidesBySubTopicId(int subTopicId)
        {
            var slides = await _slideService.FindAllSlidesBySubTopicId(subTopicId);

            return Ok(slides);
        }

        // GET: api/Slide/5
        [HttpGet("{slideId}")]
        public async Task<ActionResult> findById(int slideId)
        {
            var slide = await _slideService.FindById(slideId);

            return Ok(slide);
        }

        // POST: api/Slide
        [HttpPost]
        public async Task<ActionResult> saveSlide(SlidePost slidePost)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _slideService.SaveSlide(slidePost);

            return Created();
        }

        // PUT: api/Slide/5
        [HttpPut("{slideId}")]
        public async Task<IActionResult> EditSlide(SlideEdit slideEdit, int slideId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _slideService.EditSlide(slideEdit, slideId);

            return NoContent();
        }

        // DELETE: api/Slide/5
        [HttpDelete("{slideId}")]
        public async Task<IActionResult> DeleteSlide(int slideId)
        {
            await _slideService.DeleteSlide(slideId);

            return NoContent();
        }
    }
}
