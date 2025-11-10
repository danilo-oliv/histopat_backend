namespace histopat_back.ViewModel.SubTopic
{
    public class SubTopicGet
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string ImageUrl { get; set; } = null!;

        public ICollection<PartialSlide> Slides { get; set; } = new List<PartialSlide>();

        public int IdTopic { get; set; }

    }
}
