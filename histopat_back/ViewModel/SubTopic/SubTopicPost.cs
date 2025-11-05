using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.SubTopic
{
    public class SubTopicPost
    {
        [Required(ErrorMessage = "Informe o título")]
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(150, ErrorMessage = "Limite de 300 caracteres")]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione uma imagem")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Informe o ID do tópico")]
        public int IdTopic { get; set; }
    }
}
