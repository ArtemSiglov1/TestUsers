namespace TestUsers.Data.Models
{
    public class ProductCategoryParameterValue
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public ProductCategoryParameter ProductCategoryParameter { get; set; } = new ProductCategoryParameter();
        public int ProductCategoryParameterId { get; set; }
        public List<ProductCategoryParameterValueProduct> ProductCategoryParameterValueProduct { get; set; } = new List<ProductCategoryParameterValueProduct>(); //продукты с этим значением 
    }
}