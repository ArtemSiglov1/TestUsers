namespace TestUsers.Data.Models
{
    public class ProductCategoryParameter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // (max 100 символов)
        public ProductCategory? ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public List<ProductCategoryParameterValue> Values { get; set; } = new List<ProductCategoryParameterValue>();//все возможные значения этого параметра

    }
}