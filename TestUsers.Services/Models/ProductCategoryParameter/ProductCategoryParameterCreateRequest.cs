namespace TestUsers.Services.Models.ProductCategoryParameter
{
    public class ProductCategoryParameterCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int ProductCategoryId { get; set; }
        public List<string> Values { get; set; } = new List<string>(); //значения параметра все возможные  для этого параметра 

    }
}