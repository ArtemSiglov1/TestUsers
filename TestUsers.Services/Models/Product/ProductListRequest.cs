namespace TestUsers.Services.Models.Product
{
    public class ProductListRequest
    {
        public int? CategoryId { get; set; }
        public string? Search { get; set; }
        public decimal? FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
        public List<int> CategoryParametersValuesIds { get; set; }=new List<int>();
        public PageRequest Page { get; set; }=new PageRequest();

    }
}