namespace TestUsers.Data.Models
{
    public class ProductCategoryParameterValueProduct
    {
        public int ProductCategoryParameterValueId { get; set; }
        public ProductCategoryParameterValue? ProductCategoryParameterValue { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}