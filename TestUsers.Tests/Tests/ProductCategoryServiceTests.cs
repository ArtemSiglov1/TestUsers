using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Data;
using TestUsers.Services.Services;

namespace TestUsers.Tests.Tests
{
    public class ProductCategoryServiceTests
    {
        readonly private ProductCategoryService _service;

        Product Product { get; set; }
        public ProductCategoryServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            Product=InitToTable.InitProduct();
            db.Products.Add(Product);
            db.SaveChanges();
            _service = serviceProvider.GetRequiredService<ProductCategoryService>();
        }
        [Fact]
        public async Task SuccessGetListByParent()
        {
            var result = await _service.GetListByParent(new Services.Models.ProductCategory.CategoryGetListByParentRequest
            {
                ParentCategoryId = 1,
            });
            Assert.NotEmpty(result);
            Assert.True(result.Any(x=>x.ParentCategoryId==1),"");
        }
        [Fact]
        public async Task SuccessGetTree()
        {
            var result = await _service.GetTree();

            Assert.NotEmpty(result);

            Assert.True(result.Any(x => x.ChildCategories.Count != 0),"");


            Assert.Equal( result.Count,result.Select(x => x.Id).Distinct().Count());

            Assert.All(result, parent =>
            {
                Assert.All(parent.ChildCategories, child =>
                {
                    Assert.Equal(parent.Id, child.ParentCategoryId);
                });
            });
            Assert.Single(result);

            var root1 = result.FirstOrDefault(x => x.Id == 1);
            Assert.NotNull(root1);
            Assert.Equal("Root 1", root1.Name);
            Assert.Null(root1.ParentCategoryId);
            Assert.Equal(2, root1.ChildCategories.Count);

            var child1_1 = root1.ChildCategories.FirstOrDefault(x => x.Id == 2);
            Assert.NotNull(child1_1);
            Assert.Equal("Child 1.1", child1_1.Name);
            Assert.Equal(1, child1_1.ParentCategoryId);

            var child1_2 = root1.ChildCategories.FirstOrDefault(x => x.Id == 3);
            Assert.NotNull(child1_2);
            Assert.Equal("Child 1.2", child1_2.Name);
            Assert.Equal(1, child1_2.ParentCategoryId);

            var root2 = result.FirstOrDefault(x => x.Id == 4);
            Assert.NotNull(root2);
            Assert.Equal("Root 2", root2.Name);
            Assert.Null(root2.ParentCategoryId);
            Assert.Single(root2.ChildCategories);

            var child2_1 = root2.ChildCategories.FirstOrDefault(x => x.Id == 5);
            Assert.NotNull(child2_1);
            Assert.Equal("Child 2.1", child2_1.Name);
            Assert.Equal(4, child2_1.ParentCategoryId);
        }
        [Fact]
        public async Task SuccessCreate()
        {
            await _service.Create(new Services.Models.ProductCategory.ProductCategoryCreateRequest()
            {
                Name= "Test22",
                ParentCategoryId= 1,
            });
            await _service.Create(new Services.Models.ProductCategory.ProductCategoryCreateRequest
            {
                Name = "Test11",

            });
           var parent= await _service.GetListByParent(new Services.Models.ProductCategory.CategoryGetListByParentRequest()
            {
                ParentCategoryId=1,
                Search="Test22"
            });
            var notParent = await _service.GetListByParent(new Services.Models.ProductCategory.CategoryGetListByParentRequest
            {
                Search = "Test11"
            });
            Assert.NotEmpty(parent);
            Assert.NotEmpty(notParent);
            Assert.True(parent.Any(x => x.Name == "Test22"), "");
            Assert.True(notParent.Any(x => x.Name == "Test11"), "");
            Assert.True(parent.Any(x => x.ParentCategoryId == 1), "");
        }
        [Fact]
        public async Task SuccessUpdate()
        {
            await _service.Update(new Services.Models.ProductCategory.ProductCategoryUpdateRequest
            {
                Id = 1,
                Name = "Name",

            });
            var result = await _service.GetListByParent(new Services.Models.ProductCategory.CategoryGetListByParentRequest
            {
                Search="Name"
            });
            Assert.NotEmpty(result);
            Assert.True(result.Any(x=>x.Name=="Name"),"");
            Assert.True(result.Any(x=>x.Id==1),"");
        }
        [Fact]
        public async Task SuccessDelete()
        {
            await _service.Delete(1);
            var result = await _service.GetListByParent(new Services.Models.ProductCategory.CategoryGetListByParentRequest
            {
                Search = "Test1"
            });
            Assert.Empty(result);
        }
    }
}
