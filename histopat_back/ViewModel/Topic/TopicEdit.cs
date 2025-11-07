using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Topic
{
    public class TopicEdit
    {
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string? Title { get; set; } = null!;

        public bool? Active { get; set; }
    }
}
