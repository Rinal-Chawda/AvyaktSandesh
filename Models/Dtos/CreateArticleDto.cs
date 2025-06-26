namespace AvyaktSandesh.Models.Dtos
{
    public class CreateArticleDto
    {
        public string ArticleTitle { get; set; }
        public string Body { get; set; }
        public DateTime ArticleDate { get; set; }
        public string Language { get; set; }
        public int TitleId { get; set; }
    }
}