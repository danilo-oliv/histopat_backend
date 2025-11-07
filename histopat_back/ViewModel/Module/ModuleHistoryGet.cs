namespace histopat_back.ViewModel.Module
{
    public class ModuleHistoryGet
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }
        public int? IdUser { get; set; }
    }
}
