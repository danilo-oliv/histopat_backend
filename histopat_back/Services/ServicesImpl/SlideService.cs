using histopat_back.Context;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.Topic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class SlideService : ISlideService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SlideService> _logger;
        public SlideService(HistopatDbContext dbContext, IMapper mapper, ILogger<SlideService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<ICollection<SlideGet>> FindAllSlidesBySubTopicId(int subTopicId)
        {
            var subTopic = await _dbContext.SubTopics.AsNoTracking().Where(st => st.Id == subTopicId).FirstOrDefaultAsync();

            if (subTopic == null) throw new Exception(message: $"Não foi encontrado subtópico com id {subTopicId}");

            var slides = await _dbContext.Slides.AsNoTracking().Where(s => s.IdSubTopico == subTopicId && s.Active == true).Select(s => _mapper.Map<SlideGet>(s)).ToListAsync();

            return slides;
        }

        public async Task<SlideGet> FindById(int slideId)
        {
            var slide = await _dbContext.Slides.AsNoTracking().Where(s => s.Id == slideId && s.Active == true).Select(s => _mapper.Map<SlideGet>(s)).FirstOrDefaultAsync();

            if (slide == null) throw new Exception(message: $"Não foi encontrada lâmina com id {slideId}");

            return slide;
        }

        public async Task SaveSlide(SlidePost slidePost)
        {
            var subTopic = await _dbContext.SubTopics.AsNoTracking().Where(st => st.Id == slidePost.IdSubTopico).FirstOrDefaultAsync();

            if (subTopic == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {slidePost.IdSubTopico}");

            var slideEntity = _mapper.Map<Slide>(slidePost);

            await _dbContext.Slides.AddAsync(slideEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditSlide(SlideEdit slideEdit, int slideId)
        {
            var slideDb = await _dbContext.Slides.Where(s => s.Id == slideId).FirstOrDefaultAsync();

            if (slideDb == null) throw new Exception(message: $"Não foi encontrada lâmina com o id {slideId}");

            if (slideEdit.Title != null)
            {
                slideDb.Title = slideEdit.Title;
            }
            if (slideEdit.Description != null)
            {
                slideDb.Description = slideEdit.Description;
            }
            if (slideEdit.ImageUrl != null)
            {
                slideDb.ImageUrl = slideEdit.ImageUrl;
            }

            slideDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSlide(int slideId)
        {
            bool exists = await _dbContext.Slides.AnyAsync(s => s.Id == slideId);

            if (!exists) throw new Exception(message: $"Não foi encontrada lâmina com o id {slideId}");

            await _dbContext.Slides
                .Where(s => s.Id == slideId)
                .ExecuteUpdateAsync(u => u
                .SetProperty(x => x.Active, false));

        }
    }
}
