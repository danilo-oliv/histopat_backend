using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.SubTopic;

namespace histopat_back.Services.Interfaces
{
    public interface ISubTopicService
    {
        public Task<ICollection<SubTopicGet>> FindAllSubTopicsByTopicId(int topicId);
        public Task SaveSubTopic(SubTopicPost subTopicPost);
        public Task EditSubTopic(SubTopicEdit subTopicEdit, int subTopicId);
        public Task DeleteSubTopic(int subTopicId);
    }
}
