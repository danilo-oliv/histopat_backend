using histopat_back.ViewModel.Topic;

namespace histopat_back.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<IEnumerable<TopicGet>> FindAllTopicsByModuleId(int topicId);
        public Task<TopicGet> FindById(int topicId);
        public Task SaveTopic(TopicPost topicPost);
        public Task EditTopic(TopicEdit topicEdit, int topicId);
        public Task DeleteTopic(int topicId);
    }
}
