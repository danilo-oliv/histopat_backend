namespace histopat_back.ViewModel.Module
{
    public class ModuleGet
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string? Description { get; set; } = string.Empty;

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastModified { get; set; }

        public ICollection<ModuleHistoryGet> History { get; set; } = new List<ModuleHistoryGet>();

    }
}
