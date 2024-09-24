namespace TestUsers.Services.Models.ProductCategoryParameter
{
    public class ProductCategoryParameterValuesListRequest
    {
        public string Search { get; set; } = string.Empty;
        public int ProductCategoryParameterId { get; set; }
        public PageRequest Page { get; set; } = new PageRequest();

    }
}