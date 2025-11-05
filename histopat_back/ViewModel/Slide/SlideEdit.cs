using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Slide
{
    public class SlideEdit
    {

        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string? Title { get; set; }

        [StringLength(150, ErrorMessage = "Limite de 300 caracteres")]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
