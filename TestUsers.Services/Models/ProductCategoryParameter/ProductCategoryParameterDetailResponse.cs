namespace TestUsers.Services.Models.ProductCategoryParameter
{
    public class ProductCategoryParameterDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;
        public List<ProductCategoryParameterValueListItem> ParameterValues { get; set; } = new List<ProductCategoryParameterValueListItem>();

    }
}