namespace TestUsers.Services.Models.ProductCategory
{
    public class ProductCategoryCreateRequest
    {
        public string Name { get; set; }=string.Empty;
        public int? ParentCategoryId { get; set; }

    }
}
