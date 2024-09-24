namespace TestUsers.Data.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
        //продукты в этой категории
        public List<ProductCategory> ChildCategories { get; set; } =new List<ProductCategory>();
        //дочерние категории
        public int? ParentCategoryId { get; set; }
        public ProductCategory? ParentCategory { get; set; }
        public List<ProductCategoryParameter> Parameters { get; set; } = new List<ProductCategoryParameter>();
        //все возможные параметры категорий

    }
}