namespace AvyaktSandesh.Models
{
    public class MediaFiles
    {
        public int Id { get; set; }
        public string Type { get; set; } // image, video, audio
        public string FilePath { get; set; }
        public string Caption { get; set; }
        public string Language { get; set; } // Hindi, English
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ArticleId { get; set; }
        public Articles Article { get; set; }
    }
}