using histopat_back.Dominio.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Base
{
    public abstract class BaseHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; }
        
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public UserModel User { get; set; } = null!;

        public string Action { get; set; } = null!;
    }
}
