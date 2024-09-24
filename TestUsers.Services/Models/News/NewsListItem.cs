namespace TestUsers.Services.Models.News
{
    public class NewsListItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public int AuthorId { get; set; }
    }
}