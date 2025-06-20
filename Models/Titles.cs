namespace AvyaktSandesh.Models
{
    public class Titles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; } // Hindi, English
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Articles> Articles { get; set; } = new List<Articles>();
    }
}
