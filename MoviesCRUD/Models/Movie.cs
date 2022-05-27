using System.ComponentModel.DataAnnotations;

namespace MoviesCRUD.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<Person>? persons { get; set; }

        [Required]
        public ICollection<Genre>? genres { get; set; }
    }
}
