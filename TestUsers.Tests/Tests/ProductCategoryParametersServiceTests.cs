using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Data;
using TestUsers.Services.Services;
using TestUsers.Services.Models.ProductCategoryParameter;

namespace TestUsers.Tests.Tests
{
    public class ProductCategoryParametersServiceTests
    {
        readonly private ProductCategoryParametersService _service;
        Product Product { get; set; }
        public ProductCategoryParametersServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            Product = InitToTable.InitProduct();
            db.Products.Add(Product);
            db.SaveChanges();
            _service = serviceProvider.GetRequiredService<ProductCategoryParametersService>();
        }
        [Fact] 
        public async Task SuccessGetList()
        {
            var result=await _service.GetList(new Services.Models.ProductCategoryParameter.ProductCategoryParametersListRequest
            {
                ProductCategoryId = 1,
            });
            Assert.NotEmpty(result);
            Assert.True(result.Any(x=>x.ProductCategoryId==1),"");
        }
        [Fact]
        public async Task SuccessGetDetail()
        {
            var result = await _service.GetDetail(1);
            Assert.NotNull(result);
            Assert.Equal(1,result.Id);
        }
        [Fact]
        public async Task SuccessGetParameterValues()
        {
            var result = await _service.GetParameterValues(new Services.Models.ProductCategoryParameter.ProductCategoryParameterValuesListRequest
            {
                Page = new Services.Models.PageRequest(1, 2),
                ProductCategoryParameterId = 1,
            });
            Assert.NotNull(result);
            Assert.True(result.Items.Any(x=>x.Id==1),"");
        }
        [Fact]
        public async Task SuccessCreate()
        {
            await _service.Create(new Services.Models.ProductCategoryParameter.ProductCategoryParameterCreateRequest
            {
                ProductCategoryId = 1,
                Name = "Tests",
                Values = ["dddd", "ffff", "ggggg"]
                });
            var result = await _service.GetList(new Services.Models.ProductCategoryParameter.ProductCategoryParametersListRequest
            {
                Search = "Tests"
            });
            Assert.NotEmpty(result);
            Assert.True(result.Any(x=>x.Name=="Tests"),"");
        }
        [Fact]
        public async Task SuccessUpdate()
        {
            await _service.Update(new ProductCategoryParameterUpdateRequest
            {
                Id = 1,
                Name = "atest",
                ProductCategoryId = 1,
                Values = ["aa"]
            });
            var result = await _service.GetDetail(1);
            Assert.NotNull(result);
            Assert.Equal("atest",result.Name);
            Assert.True(result.ParameterValues.Any(x=>x.Id==1),"");
            Assert.True( result.ParameterValues.Any(x=>x.Name== "aa"),"");
            
        }
        [Fact]
        public async Task SuccessDelete()
        {
            await _service.Delete(1);
            await _service.Delete(2);
            Assert.Null(await _service.GetDetail(1));
            Assert.Null(await _service.GetDetail(2));
        }
    }
}
