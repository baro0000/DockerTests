using System.ComponentModel.DataAnnotations;

namespace SOTags.DataAccess.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
