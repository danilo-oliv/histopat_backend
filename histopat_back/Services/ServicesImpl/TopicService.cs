using histopat_back.Context;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Topic;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Services.ServicesImpl
{
    public class TopicService : ITopicService
    {
        private readonly HistopatDbContext _dbContext;
        private readonly IMapper _mapper;

        public TopicService(HistopatDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TopicGet> FindAllTopicsById(int topicId)
        {
            try
            {
                var topic = await _dbContext.Topics
                    .AsNoTracking()
                    .Where(t => t.Id == topicId && t.Active == true)
                    .FirstOrDefaultAsync();

                if (topic == null)
                    throw new Exception($"Não foi encontrado tópico com o id {topicId}");

                return _mapper.Map<TopicGet>(topic);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar o tópico: {ex}");
            }
        }

        public async Task<TopicGet> FindModuleByIdAsync(int moduleId)
        {
            try
            {
                var topics = await _dbContext.Topics
                    .AsNoTracking()
                    .Where(t => t.IdModule == moduleId && t.Active == true)
                    .Select(t => _mapper.Map<TopicGet>(t))
                    .ToListAsync();

                // Aqui retornamos o primeiro só porque o método pede TopicGet (um único),
                // mas o ideal seria alterar o método para retornar uma lista.
                return topics.FirstOrDefault() ?? new TopicGet();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tópicos do módulo: {ex}");
            }
        }

        public async Task SaveTopic(TopicPost topicPost)
        {
            try
            {
                var topicEntity = _mapper.Map<Topic>(topicPost);
                topicEntity.CreatedAt = DateTime.Now;
                topicEntity.LastModified = DateTime.Now;
                topicEntity.Active = true;

                await _dbContext.Topics.AddAsync(topicEntity);
                await _dbContext.SaveChangesAsync();

                // Cria histórico
                var history = new TopicHistory
                {
                    IdTopic = topicEntity.Id,
                    ChangedAt = DateTime.Now,
                    IdUser = 1, // temporário
                    Action = "Created"
                };

                await _dbContext.Set<TopicHistory>().AddAsync(history);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar tópico: {ex}");
            }
        }

        public async Task EditTopic(TopicEdit topicEdit, int topicId)
        {
            try
            {
                var topicDb = await _dbContext.Topics
                    .Where(t => t.Id == topicId)
                    .FirstOrDefaultAsync();

                if (topicDb == null)
                    throw new Exception($"Não foi encontrado tópico com o id {topicId}");

                if (!string.IsNullOrEmpty(topicEdit.Title))
                    topicDb.Title = topicEdit.Title;

                if (topicEdit.Active.HasValue)
                    topicDb.Active = topicEdit.Active.Value;

                topicDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                // Cria histórico
                var history = new TopicHistory
                {
                    IdTopic = topicDb.Id,
                    ChangedAt = DateTime.Now,
                    IdUser = 1, // temporário
                    Action = "Updated"
                };

                await _dbContext.Set<TopicHistory>().AddAsync(history);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao editar o tópico: {ex}");
            }
        }


        public async Task DeleteTopic(int topicId)
        {
            try
            {
                var topicDb = await _dbContext.Topics
                    .Where(t => t.Id == topicId)
                    .FirstOrDefaultAsync();

                if (topicDb == null)
                    throw new Exception($"Não foi encontrado tópico com o id {topicId}");

                topicDb.Active = false;
                topicDb.LastModified = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                // Cria histórico
                var history = new TopicHistory
                {
                    IdTopic = topicDb.Id,
                    ChangedAt = DateTime.Now,
                    IdUser = 1, // temporário
                    Action = "Deleted"
                };

                await _dbContext.Set<TopicHistory>().AddAsync(history);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o tópico: {ex}");
            }
        }
    }
}
