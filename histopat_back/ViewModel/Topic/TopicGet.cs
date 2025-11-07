namespace histopat_back.ViewModel.Topic
{
    public class TopicGet
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int IdModule { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastModified { get; set; }
    }

}
