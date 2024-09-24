namespace TestUsers.Services.Models.ProductCategory
{
    public class ProductCategoryUpdateRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int? ParentCategoryId { get; set; }

    }
}
