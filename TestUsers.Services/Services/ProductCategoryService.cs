using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models;
using Microsoft.EntityFrameworkCore;
using TestUsers.Data;
using TestUsers.Services.Models.ProductCategory;
using TestUsers.Services.Validators.ProductCategoryValid;
using FluentValidation;
using TestUsers.Data.Models;

namespace TestUsers.Services.Services
{
    public class ProductCategoryService
    {
       readonly private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public ProductCategoryService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<List<ProductCategoryListItem>> GetListByParent(CategoryGetListByParentRequest request)
        {

            await using var db = new DataContext(_dbContextOptions);
            var query = db.ProductCategories.AsQueryable();

           if(!string.IsNullOrEmpty(request.Search))
               query=query.Where(product=> product.Name.Contains(request.Search));

            var productCategoryList =await query.Where(x => x.ParentCategoryId == request.ParentCategoryId)
                .Select(x => new ProductCategoryListItem() { Id = x.Id, Name = x.Name, ParentCategoryId = request.ParentCategoryId })
                .ToListAsync();
            return productCategoryList;

        }//получить список категорий по родительской категории
        public async Task<List<ProductCategoryTreeItem>> GetTree()
        {
            await using var db = new DataContext(_dbContextOptions);
            var allCategories =await db.ProductCategories.ToListAsync();
            var product =await GetForParentsIds(allCategories, null);

            return product;
        }  //получить дерево категорий

        public static async Task<List<ProductCategoryTreeItem>> GetForParentsIds(List<ProductCategory> categories, params int[]? parentsIds)
        {
            List<ProductCategory> parentQuery;
            if (parentsIds == null)
                parentQuery = categories.Where(x => x.ParentCategoryId == null).ToList();
            else
                parentQuery = categories.Where(x => x.ParentCategoryId.HasValue
                    && parentsIds.Contains(x.ParentCategoryId.Value)).ToList();

            var parentList =  parentQuery.Select(x => new ProductCategoryTreeItem()
            {
                Id = x.Id,
                Name = x.Name,
                ParentCategoryId = x.ParentCategoryId,

            }).ToList();
           var ids=parentList.Select(x => x.Id).ToArray();
            if (ids.Length==0)
                return parentList;
            var children=await GetForParentsIds(categories, ids);
            foreach (var parent in parentList)
            {
                parent.ChildCategories=children.Where(x=>x.ParentCategoryId==parent.Id).ToList();
            }
            return parentList;
        }
        public async Task<BaseResponse> Create(ProductCategoryCreateRequest request)
        {
            var valid = new CreateProductCategoryValidator();
            await valid.ValidateAndThrowAsync(request);
            await using var db = new DataContext(_dbContextOptions);
           await db.ProductCategories.AddAsync(new ProductCategory()
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId,
            });
           await db.SaveChangesAsync();
            return new BaseResponse(true);
        }
        public async Task<BaseResponse> Update(ProductCategoryUpdateRequest request)
        {
            var valid = new UpdateProductCategoryValidator(); 
            await valid.ValidateAndThrowAsync(request);
            await using var db = new DataContext(_dbContextOptions);
            var category=await db.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category != null&&request.Name!=null)
            category.Name = request.Name;
            if (category != null && request.ParentCategoryId!=null)
            category.ParentCategoryId = request.ParentCategoryId;
                await db.SaveChangesAsync();

            return new BaseResponse(true);
        }
        public async Task<BaseResponse> Delete(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var productCategory =await db.ProductCategories.FirstOrDefaultAsync(_ => _.Id == id);
            if (productCategory != null) 
                db.ProductCategories.Remove(productCategory);
              
            await db.SaveChangesAsync();
            return new BaseResponse(true);
        }// должен удалять все вложенные категории тоже

    }
}
