namespace TestUsers.Services.Models.News
{
    public class NewsCreateRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public string Tags { get; set; }=string.Empty;
    }
}
