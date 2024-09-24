namespace TestUsers.Services.Models.Product
{
    public class ProductSaveRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> CategoryParametersValuesIds { get; set; } = new List<int>();




    }
}