using histopat_back.ViewModel.Slide;
using histopat_back.ViewModel.SubTopic;

namespace histopat_back.Services.Interfaces
{
    public interface ISubTopicService
    {
        public Task<SubTopicGet> FindAllSubTopicsById(int subTopicId);
        public void SaveSubTopic(SubTopicPost subTopicPost);
        public void EditSubTopic(SubTopicPost subTopicEdit);
        public void DeleteSubTopic(int subTopicId);
    }
}
