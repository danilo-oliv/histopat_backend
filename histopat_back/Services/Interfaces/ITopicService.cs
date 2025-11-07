using histopat_back.ViewModel.Topic;

namespace histopat_back.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<TopicGet> FindAllTopicsById(int topicId);
        public Task<TopicGet> FindModuleByIdAsync(int moduleId);
        public Task SaveTopic(TopicPost topicPost);
        public Task EditTopic(TopicEdit topicEdit, int topicId);
        public Task DeleteTopic(int topicId);
    }
}
