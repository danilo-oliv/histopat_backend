using histopat_back.Context;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Slide;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class SlideService : ISlideService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        public SlideService(HistopatDbContext dbContext, IMapper mapper) {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ICollection<SlideGet>> FindAllSlidesBySubTopicId(int subTopicId)
        {
            try
            {
                var slides = await _dbContext.Slides.AsNoTracking().Where(s => s.IdSubTopico == subTopicId).Select(s => _mapper.Map<SlideGet>(s)).ToListAsync();

                return slides;
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao buscar lâminas: {ex.ToString}");
            }
        }

        public async Task SaveSlide(SlidePost slidePost)
        {
            try
            {
                var slideEntity = _mapper.Map<Slide>(slidePost);

                await _dbContext.Slides.AddAsync(slideEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao salvar a lâmina: {ex.ToString}");
            }
        }

        public async Task EditSlide(SlideEdit slideEdit, int slideId)
        {
            try
            {
                var slideDb = await _dbContext.Slides.AsNoTracking().Where(s => s.Id == slideId).FirstOrDefaultAsync();

                if (slideDb == null) throw new Exception(message: $"Não foi encontrada lâmina com o id {slideId}");

                if(slideEdit.Title != null)
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
            catch (Exception ex) {
                throw new Exception(message: $"Erro ao editar a lâmina: {ex.ToString}");
            }
        }

        public async Task DeleteSlide(int slideId)
        {
            try
            {
                var slideDb = await _dbContext.Slides.AsNoTracking().Where(s => s.Id == slideId).FirstOrDefaultAsync();

                if (slideDb == null) throw new Exception(message: $"Não foi encontrada lâmina com o id {slideId}");

                slideDb.Active = false;
                slideDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao excluir a lâmina: {ex.ToString}");
            }
        }
    }
}
