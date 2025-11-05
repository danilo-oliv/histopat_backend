using histopat_back.ViewModel.Topic;

namespace histopat_back.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<TopicGet> FindAllTopicsById(int topicId);
        public void SaveTopic(TopicPost topicPost);
        public void EditTopic(TopicPost topicEdit);
        public void DeleteTopic(int topicId);
    }
}
