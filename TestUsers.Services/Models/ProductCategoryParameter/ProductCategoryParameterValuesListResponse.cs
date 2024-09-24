namespace TestUsers.Services.Models.ProductCategoryParameter
{
    public class ProductCategoryParameterValuesListResponse
    {
        public List<ProductCategoryParameterValueListItem> Items { get; set; } = new List<ProductCategoryParameterValueListItem>();
        public PageResponse Page { get; set; } = new PageResponse();

    }
}