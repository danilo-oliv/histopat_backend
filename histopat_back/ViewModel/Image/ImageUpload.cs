using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Image
{
    public class ImageUpload
    {
        [Required(ErrorMessage = "Selecione uma image")]
        public IFormFile File { get; set; } = null!;

        [Required(ErrorMessage = "Informe o nome da imagem")]
        public string CustomFileName { get; set; } = null!;
    }
}
