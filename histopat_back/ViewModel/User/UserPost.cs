using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.User
{
    public class UserPost
    {
        [Required(ErrorMessage = "Informe o nome do usuário")]
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string Name { get; set; } = null!;

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "Informe o username")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; } = null!;

        public int? RoleId { get; set; }
    }
}
