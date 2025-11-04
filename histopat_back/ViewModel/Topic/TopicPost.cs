using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Topic
{
    public class TopicPost
    {
        [Required(ErrorMessage = "Informe o título")]
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string Title { get; set; } = null!;

    }
}
