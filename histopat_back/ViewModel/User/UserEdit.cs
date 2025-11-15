using System.ComponentModel.DataAnnotations;

namespace histopat_back.ViewModel.User
{
    public class UserEdit
    {
        [StringLength(150, ErrorMessage = "Limite de 150 caracteres")]
        public string? Name { get; set; }

        public bool? Active { get; set; }

        public ICollection<byte>? Roles { get; set; }
    }
}
