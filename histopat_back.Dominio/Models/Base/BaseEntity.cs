using System.ComponentModel.DataAnnotations;

namespace histopat_back.Dominio.Models.Base
{
    /** BaseEntity is an abstract class that represents the common properties of Module, Topic and Subtopic in the system.
     * It has a generic type parameter THistory that represents the type of history associated with the entity.
     * THistory must be a subclass of BaseHistory.
     */
    public abstract class BaseEntity<THistory> where THistory : BaseHistory
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastModified { get; set; }
        /**
         * History is a collection of THistory objects that represent the history of changes made to the entity. Can be ModuleHistory, TopicHistory or SubtopicHistory.
         */
        public ICollection<THistory> History { get; set; } = new List<THistory>();


    }
}
