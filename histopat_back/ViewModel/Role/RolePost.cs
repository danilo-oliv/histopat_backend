using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Role
{
    public class RolePost
    {
        [Required(ErrorMessage = "Informe o nome da função")]
        [StringLength(50, ErrorMessage = "Limite de 50 caracteres")]
        public string Name { get; set; } = null!;

        public bool Active { get; set; } = true;
    }
}
