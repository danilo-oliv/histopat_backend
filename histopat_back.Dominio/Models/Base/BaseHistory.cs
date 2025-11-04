using histopat_back.Dominio.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Base
{
    public abstract class BaseHistory
    {
        public int Id { get; set; }

        public DateTime ChangedAt { get; set; }
        
        public int IdUser { get; set; }

        public User.User User { get; set; } = null!;

        public string Action { get; set; } = null!;
    }
}
