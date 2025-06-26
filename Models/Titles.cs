using System.ComponentModel.DataAnnotations;

namespace AvyaktSandesh.Models
{
    public class Titles
    {
        public int Id { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]

        public string Language { get; set; } // Hindi, English
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Articles> Articles { get; set; } = new List<Articles>();
    }
}
