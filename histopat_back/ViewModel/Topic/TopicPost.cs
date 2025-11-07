using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Topic
{
    public class TopicPost
    {
        [Required(ErrorMessage = "Informe o título do tópico")]
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Informe o módulo associado")]
        public int IdModule { get; set; }

        [Required(ErrorMessage = "Informe se o tópico está ativo ou não")]
        public bool Active { get; set; }
    }
}
