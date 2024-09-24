namespace TestUsers.Data.Models
{
    public class Product
    {
       public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //(max 100 символов)
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public List<ProductCategoryParameterValueProduct> ProductCategoryParameterValueProduct { get; set; } = new List<ProductCategoryParameterValueProduct>(); // значения параметров товара (например 5кг, Xl, зеленый)
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = new ProductCategory();

    }
}
