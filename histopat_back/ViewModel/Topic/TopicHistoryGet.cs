namespace histopat_back.ViewModel.Topic
{
    public class TopicHistoryGet
    {
        public int Id { get; set; }

        public string Action { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; }

        public int? IdUser { get; set; }
    }
}
