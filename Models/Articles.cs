using System.Text.Json.Serialization;

namespace AvyaktSandesh.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string ArticleTitle { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TitleId { get; set; }

        [JsonIgnore]
        public Titles Title { get; set; }
        public ICollection<MediaFiles> MediaFiles { get; set; } = new List<MediaFiles>();
    }
}
