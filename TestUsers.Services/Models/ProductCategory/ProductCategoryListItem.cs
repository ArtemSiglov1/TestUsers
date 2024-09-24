namespace TestUsers.Services.Models.ProductCategory
{
    public class ProductCategoryListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }

    }
}
