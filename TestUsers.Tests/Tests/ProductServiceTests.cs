using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Services;

namespace TestUsers.Tests.Tests
{
    public class ProductServiceTests
    {
        readonly private ProductService _service;

        Product Product { get; set; }
        public ProductServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            Product = InitToTable.InitProduct();
            db.Products.Add(Product);
            db.SaveChanges();
            _service = serviceProvider.GetRequiredService<ProductService>();
        }
        [Fact]
        public async Task SuccessGetList()
        {
            var list = await _service.GetList(new Services.Models.Product.ProductListRequest
            {
                CategoryId = Product.CategoryId,
                Search="test",
                Page=new Services.Models.PageRequest
                {
                    Page=1,
                    PageSize=10,
                }
            });
            Assert.NotNull(list);
            Assert.NotEmpty(list.Items);
            Assert.Contains(list.Items, x => x.Name.Contains("test"));
            Assert.Contains(list.Items,x=>x.CategoryId== Product.CategoryId);
            list = await _service.GetList(new Services.Models.Product.ProductListRequest
            {
                FromAmount = 100,
                ToAmount = 1,
            });
            Assert.Empty(list.Items);
        }
        [Fact]
        public async Task SuccessGetDetail()
        {
            var detail = await _service.GetDetail(1);
            Assert.NotNull(detail);
            Assert.Equal("Test", detail.Name);
            Assert.Equal(1, detail.Id);
        }
        [Fact]
        public async Task SuccessSave()
        {
            await _service.Save(new Services.Models.Product.ProductSaveRequest
            {

                CategoryId = 2,
                 Amount = 10,
                 CategoryName = "tests",
                 DateCreated = DateTime.Now,
                 Description = "Tests",
                  Name = "tests",
                CategoryParametersValuesIds = [1, 2, 3]
            });
            var res = await _service.GetDetail(2);
            Assert.NotNull(res);
            Assert.Equal("tests", res.Name);
            Assert.Equal(2, res.Id);
            Assert.Equal(10, res.Amount);
            Assert.Equal("Tests", res.Description);
            await _service.Save(new Services.Models.Product.ProductSaveRequest
            {
                DateCreated = DateTime.Now,
                Description = "t",
                Amount = 11,
                CategoryId = 1,
                Id = 2,
                CategoryParametersValuesIds = [1, 2, 3],
                CategoryName = "t",
                Name = "t"
            });
            res= await _service.GetDetail(2); Assert.NotNull(res);
            Assert.Equal("t", res.Name);
            Assert.Equal(2, res.Id);
            Assert.Equal(11, res.Amount);
            Assert.Equal("t", res.Description);
            Assert.Equal(1, res.CategoryId);


        }
    }
}
