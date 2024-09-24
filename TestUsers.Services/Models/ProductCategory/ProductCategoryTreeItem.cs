namespace TestUsers.Services.Models.ProductCategory
{
    public class ProductCategoryTreeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public int? ParentCategoryId { get; set; }
        public List<ProductCategoryTreeItem> ChildCategories { get; set; }=new List<ProductCategoryTreeItem>();

    }
}
