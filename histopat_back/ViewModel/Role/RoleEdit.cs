using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.Role
{
    public class RoleEdit
    {
        [StringLength(50, ErrorMessage = "Limite de 50 caracteres")]
        public string? Name { get; set; }

        public bool? Active { get; set; }
    }
}
