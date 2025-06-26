using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AvyaktSandesh.Models
{
    public class Articles
    {
        public int Id { get; set; }
        [Required]
        public string ArticleTitle { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime ArticleDate { get; set; }

        [Required]
        public string Language { get; set; } // Hindi, English
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //[Required]
        public int TitleId { get; set; }

        [JsonIgnore]        
        public Titles Title { get; set; }
        public ICollection<MediaFiles> MediaFiles { get; set; } = new List<MediaFiles>();
    }
}
