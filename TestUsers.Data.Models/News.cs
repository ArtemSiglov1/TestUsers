namespace TestUsers.Data.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public User Author { get; set; }=new User();
        public int AuthorId { get; set; }
        public List<NewsTagsRelation> Tags { get; set; }= new List<NewsTagsRelation>();

    }
}
