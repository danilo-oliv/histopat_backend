using histopat_back.Context;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.SubTopic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class SubTopicService : ISubTopicService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        public SubTopicService(HistopatDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ICollection<SubTopicGet>> FindAllSubTopicsByTopicId(int topicId)
        {
            try
            {
                var subTopics = await _dbContext.SubTopics.AsNoTracking().Where(st => st.IdTopic == topicId && st.Active == true).Select(st => _mapper.Map<SubTopicGet>(st)).ToListAsync();

                return subTopics;
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao buscar subtópicos: {ex.ToString}");
            }
        }

        public async Task SaveSubTopic(SubTopicPost subTopicPost)
        {
            try
            {
                var subTopicEntity = _mapper.Map<Subtopic>(subTopicPost);

                await _dbContext.SubTopics.AddAsync(subTopicEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao salvar subtópicos: {ex.ToString}");
            }
        }

        public async Task EditSubTopic(SubTopicEdit subTopicEdit, int subTopicId)
        {
            try
            {
                var subTopicDb = await _dbContext.SubTopics.Where(st=> st.Id == subTopicId).FirstOrDefaultAsync();

                if (subTopicDb == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {subTopicId}");

                if (subTopicEdit.Title != null)
                {
                    subTopicDb.Title = subTopicEdit.Title;
                }
                if (subTopicEdit.Description != null)
                {
                    subTopicDb.Description = subTopicEdit.Description;
                }
                if (subTopicEdit.ImageUrl != null)
                {
                    subTopicDb.ImageUrl = subTopicEdit.ImageUrl;
                }

                subTopicDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao editar o subtópico: {ex.ToString}");
            }
        }

        public async Task DeleteSubTopic(int subTopicId)
        {
            try
            {
                var subTopicDb = await _dbContext.SubTopics.Where(st => st.Id == subTopicId).FirstOrDefaultAsync();

                if (subTopicDb == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {subTopicId}");

                subTopicDb.Active = false;
                subTopicDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(message: $"Erro ao excluir o subTópico: {ex.ToString}");
            }
        }
    }
}
