using histopat_back.Context;
using histopat_back.Dominio.Models.Subtopic;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.SubTopic;
using histopat_back.ViewModel.Topic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class TopicService : ITopicService
    {
        private HistopatDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TopicService> _logger;
        public TopicService(HistopatDbContext dbContext, IMapper mapper, ILogger<TopicService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<TopicGet>> FindAllTopicsByModuleId(int moduleId)
        {
            var module = await _dbContext.Modules.AsNoTracking().Where(m => m.Id == moduleId).FirstOrDefaultAsync();

            if (module == null) throw new Exception(message: $"Não foi encontrado módulo com o id {moduleId}");

            var topics = await _dbContext.Topics.AsNoTracking().Where(t => t.IdModule == moduleId && t.Active == true).Include(t => t.SubTopics.Where(st => st.Active == true)).Select(t => _mapper.Map<TopicGet>(t)).ToListAsync();

            return topics;
        }

        public async Task<TopicGet> FindById(int topicId)
        {
            var topic = await _dbContext.Topics.AsNoTracking().Where(t => t.Id == topicId && t.Active == true).Include(t => t.SubTopics.Where(st => st.Active == true)).Select(t => _mapper.Map<TopicGet>(t)).FirstOrDefaultAsync();

            if (topic == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {topicId}");

            return topic;
        }

        public async Task SaveTopic(TopicPost topicPost)
        {
            var module = await _dbContext.Modules.AsNoTracking().Where(m => m.Id == topicPost.IdModule).FirstOrDefaultAsync();

            if (module == null) throw new Exception(message: $"Não foi encontrado módulo com o id {topicPost.IdModule}");

            var topicEntity = _mapper.Map<Topic>(topicPost);

            await _dbContext.Topics.AddAsync(topicEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditTopic(TopicEdit TopicEdit, int idTopic)
        {
            var topicDb = await _dbContext.Topics.Where(t => t.Id == idTopic).FirstOrDefaultAsync();

            if (topicDb == null) throw new Exception(message: $"Não foi encontrado subtópico com o id {idTopic}");

            if (TopicEdit.Title != null)
            {
                topicDb.Title = TopicEdit.Title;
            }

            topicDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTopic(int topicId)
        {
            var TopicDb = await _dbContext.Topics.Where(t => t.Id == topicId).FirstOrDefaultAsync();

            if (TopicDb == null) throw new Exception(message: $"Não foi encontrado tópico com o id {topicId}");

            TopicDb.Active = false;
            TopicDb.LastModified = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalTopics()
        {
            return await _dbContext.Topics
                .CountAsync(t => t.Active == true);
        }
    }
}
