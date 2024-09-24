namespace TestUsers.Services.Models.Product
{
    public class ProductListResponse
    {
        public List<ProductListItem> Items { get; set; } = new List<ProductListItem>();
        public PageResponse Page { get; set; }= new PageResponse();
    }
}