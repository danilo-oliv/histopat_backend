namespace histopat_back.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public bool IsActive { get; set; } 
        public int Size { get; set; }

        }
}
