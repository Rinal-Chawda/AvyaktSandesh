using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AvyaktSandesh.Models
{
    public class MediaFiles
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } // image, video, audio
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string Caption { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ArticleId { get; set; }
        [JsonIgnore]
        public Articles Article { get; set; }
    }
}