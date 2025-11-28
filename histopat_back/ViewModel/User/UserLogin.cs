using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Informe o username")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; } = null!;
    }
}
