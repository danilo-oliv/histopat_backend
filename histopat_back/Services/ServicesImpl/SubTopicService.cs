using histopat_back.Context;
using histopat_back.Dominio.Models.Slide;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Middlewares;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.SubTopic;
using histopat_back.ViewModel.Topic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class SubTopicService : ISubTopicService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SubTopicService> _logger;
        public SubTopicService(HistopatDbContext dbContext, IMapper mapper, ILogger<SubTopicService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<SubTopicGet>> FindAllSubTopicsByTopicId(int topicId)
        {
            var topic = await _dbContext.Topics.AsNoTracking().Where(t => t.Id == topicId).FirstOrDefaultAsync();

            if (topic == null) throw new Exception(message: $"Não foi encontrado tópico com id {topicId}");

            var subTopics = await _dbContext.SubTopics.AsNoTracking().Where(st => st.IdTopic == topicId && st.Active == true).Select(st => _mapper.Map<SubTopicGet>(st)).ToListAsync();

            return subTopics;
        }

        public async Task<SubTopicGet> findById(int subTopicId)
        {
            var subTopic = await _dbContext.SubTopics.AsNoTracking().Where(st => st.Id == subTopicId && st.Active == true).Select(st => _mapper.Map<SubTopicGet>(st)).FirstOrDefaultAsync();

            if (subTopic == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {subTopicId}");

            return subTopic;
        }

        public async Task SaveSubTopic(SubTopicPost subTopicPost)
        {
            var topic = await _dbContext.Topics.AsNoTracking().Where(t => t.Id == subTopicPost.IdTopic).FirstOrDefaultAsync();

            if (topic == null) throw new Exception(message: $"Não foi encontrado tópico com o id {subTopicPost.IdTopic}");

            var subTopicEntity = _mapper.Map<Subtopic>(subTopicPost);

            await _dbContext.SubTopics.AddAsync(subTopicEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditSubTopic(SubTopicEdit subTopicEdit, int subTopicId)
        {
            var subTopicDb = await _dbContext.SubTopics.Where(st => st.Id == subTopicId).FirstOrDefaultAsync();

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

        public async Task DeleteSubTopic(int subTopicId)
        {
            var subTopicDb = await _dbContext.SubTopics.Where(st => st.Id == subTopicId).FirstOrDefaultAsync();

            if (subTopicDb == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {subTopicId}");

            subTopicDb.Active = false;
            subTopicDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
