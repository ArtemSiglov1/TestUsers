namespace TestUsers.Data.Models
{
    public class NewsTagsRelation
    {
        public int NewsId { get; set; }
        public News News { get; set; } = new News();
        public int NewsTagId { get; set; }
        public NewsTag NewsTag { get; set; } = new NewsTag();

    }
}