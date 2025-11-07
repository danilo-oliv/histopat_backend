using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.SubTopic;

namespace histopat_back.Services.Interfaces
{
    public interface ISubTopicService
    {
        public Task<IEnumerable<SubTopicGet>> FindAllSubTopicsByTopicId(int topicId);
        public Task<SubTopicGet> findById(int subTopicId);
        public Task SaveSubTopic(SubTopicPost subTopicPost);
        public Task EditSubTopic(SubTopicEdit subTopicEdit, int subTopicId);
        public Task DeleteSubTopic(int subTopicId);
    }
}
