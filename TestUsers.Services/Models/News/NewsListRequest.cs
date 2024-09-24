namespace TestUsers.Services.Models.News
{
    public class NewsListRequest
    {
        public string? Search { get; set; } = string.Empty; //строка поиска новостей по имени, имейлу, тегам	

        public int? TagId { get; set; } // поиск по определенному тэгу

        public PageRequest Page { get; set; } = new PageRequest();
    }
}
