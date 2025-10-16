using histopat_back.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Models.Base
{
    public abstract class BaseHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime ChangedAt { get; set; }
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public UserModel User { get; set; } = null!;

        public string Action { get; set; }
    }
}
