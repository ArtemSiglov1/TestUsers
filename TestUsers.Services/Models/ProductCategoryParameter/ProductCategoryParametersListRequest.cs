namespace TestUsers.Services.Models.ProductCategoryParameter
{
    public class ProductCategoryParametersListRequest
    {
        public string Search { get; set; } = string.Empty;
        public int? ProductCategoryId { get; set; }
    }
}