using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Module
{
    public class ModuleEdit
    {
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string Title { get; set; } = null!;

        [StringLength(150, ErrorMessage = "Limite de 300 caracteres")]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = null!;
    }
}
