using System.ComponentModel.DataAnnotations;

namespace MoviesCRUD.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}
