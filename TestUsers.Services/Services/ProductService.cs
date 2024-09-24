using FluentValidation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;

using TestUsers.Data.Models;
using TestUsers.Services.Extensions;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Product;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Validators.ProductValid;
using TestUsers.Services.Validators.UserContactValid;

namespace TestUsers.Services.Services
{
    public class ProductService
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public ProductService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<ProductListResponse> GetList(ProductListRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Products.AsQueryable();

            if (request == null)
            { return new ProductListResponse(); }
            //исправить но как подумаю
            if (!string.IsNullOrEmpty(request.Search))
                    query = query.Where(x =>x.Name.Contains(request.Search));
           var resu= await query.ToListAsync();
            if (request.CategoryId != null)
                    query = query.Where(x => x.CategoryId == request.CategoryId);
           resu= await query.ToListAsync();
            if (request.ToAmount != null)
                    query=query.Where(x=>x.Amount>=request.ToAmount);
            resu = await query.ToListAsync();
            if (request.FromAmount != null)
                    query = query.Where(x => x.Amount <= request.FromAmount);
            resu = await query.ToListAsync();
            var res = await query.GetPage(request.Page,
                    x => new ProductListItem()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CategoryId = x.CategoryId,
                    CategoryName=x.ProductCategory.Name,
                    DateCreated = x.DateCreated,
                    Name=x.Name,
                }).ToListAsync();
                int count= res.Count;
                var response = new ProductListResponse()
                {
                    Items = res,
                    Page=new PageResponse(
                        request.Page,
                        count
                        )
                };
                return response;
            
        } //получить список 
        public async Task<ProductDetailResponse?> GetDetail(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var result=await db.Products.Where(x=>x.Id==id).Select(x => new ProductDetailResponse()
            {
                DateCreated = x.DateCreated,
                Description = x.Description,
                Amount=x.Amount,
                CategoryId=x.CategoryId,
                CategoryName=x.ProductCategory.Name,
                Name=x.Name,
                Id=id
            }).FirstOrDefaultAsync();
            return result;
        }  //получить детальную

        public async Task<BaseResponse?> Save(ProductSaveRequest request)
        {
            await using var db= new DataContext(_dbContextOptions);
           var result=request.Id == null ? await Create(request, db) :await Update(request, db);

            return  result;
        }
        private static async Task<BaseResponse> Create(ProductSaveRequest request, DataContext db)
        {
            var validations = new ProductSaveRequestValidator();
          await validations.ValidateAndThrowAsync(request);
            Product? product;

            
                product = new Product()
                {
                    DateCreated = request.DateCreated,
                    Description = request.Description,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                    Name = request.Name,
                    ProductCategoryParameterValueProduct = request.CategoryParametersValuesIds
                        .Select(id => new ProductCategoryParameterValueProduct
                        {
                            ProductCategoryParameterValueId = id
                        }).ToList()
                };

                var category = await db.ProductCategories
                    .FirstOrDefaultAsync(c => c.Id == request.CategoryId);
            if (category != null)
            {
                product.ProductCategory = category;
                product.CategoryId = request.CategoryId;
            }
            else
                return new BaseResponse(false, "категории с данным айди не существует");


            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return new BaseResponse(true);

        }
        private static async Task<BaseResponse> Update(ProductSaveRequest request, DataContext db)
        {
            var validations = new ProductSaveRequestValidator();
            await validations.ValidateAndThrowAsync(request);

            var product = await db.Products
                .Include(p => p.ProductCategoryParameterValueProduct) 
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
                return new BaseResponse(false, "Product not found");

            product.Description = request.Description;
            product.Amount = request.Amount;
            product.Name = request.Name;

            var category = await db.ProductCategories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);
            if (category != null)
            {
                product.ProductCategory = category;
                product.CategoryId = request.CategoryId;
            }
            else
                return new BaseResponse(false, "Категории с данным айди не существует");

            var newParameterValues = request.CategoryParametersValuesIds.Select(id => new ProductCategoryParameterValueProduct
            {
                ProductCategoryParameterValueId = id,
            }).ToList();

            db.ProductCategoryParameterValueProducts.RemoveRange(product.ProductCategoryParameterValueProduct);
            product.ProductCategoryParameterValueProduct = newParameterValues;

            db.Products.Update(product);

            await db.SaveChangesAsync();

            return new BaseResponse(true);
        }
        public async Task<BaseResponse?> Delete(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            await db.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
            await db.SaveChangesAsync();
            return new BaseResponse(true);
        }

    }
}
