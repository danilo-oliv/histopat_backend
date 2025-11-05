using histopat_back.ViewModel.Slide;

namespace histopat_back.Services.Interfaces
{
    public interface ISlideService
    {
        public Task<ICollection<SlideGet>> FindAllSlidesBySubTopicId(int subTopicId);
        public Task SaveSlide(SlidePost slidePost);
        public Task EditSlide(SlideEdit slideEdit, int slideId);
        public Task DeleteSlide(int slideId);
    }
}
