using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Extensions;
using TestUsers.Services.Models;
using TestUsers.Services.Models.ProductCategoryParameter;
using TestUsers.Services.Validators.CategoryParametersValid;

namespace TestUsers.Services.Services
{
    public class ProductCategoryParametersService
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public ProductCategoryParametersService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<List<ProductCategoryParameterListItem>> GetList(ProductCategoryParametersListRequest request)  //получить список параметров категории
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.ProductCategoryParameters.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(x =>x.Name.Contains(request.Search));
            if (request.ProductCategoryId != null)
                query = query.Where(x => x.ProductCategoryId == request.ProductCategoryId);
            var items = await query.
                Select(x => new ProductCategoryParameterListItem()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     ProductCategoryId = x.ProductCategoryId,
                 })
                 .ToListAsync();

            return items;
        }
        public async Task<ProductCategoryParameterDetailResponse?> GetDetail(int id)  //получить список параметров категории
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = await db.ProductCategoryParameters
                .Where(query => query.Id == id)
                .Select(x => new ProductCategoryParameterDetailResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductCategoryId = x.ProductCategoryId,
                    ParameterValues = x.Values.Select(x => new ProductCategoryParameterValueListItem()
                    {
                        Id = x.Id,
                        Name = x.Value,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return query;

        }
        public async Task<ProductCategoryParameterValuesListResponse> GetParameterValues(ProductCategoryParameterValuesListRequest request)  //получить список значений параметра категории
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.ProductCategoryParameters.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
                query=query.Where(x=> x.Name.Contains(request.Search)); 

            var count = query.Count();

            var items =await query
                .Where(x => x.Id == request.ProductCategoryParameterId)
                .GetPage(request.Page,
                product => new ProductCategoryParameterValueListItem
                {
                    Id = product.Id,
                    Name = product.Name,

                })
                .ToListAsync();

            return new ProductCategoryParameterValuesListResponse
            {
               Items = items,
                Page = new PageResponse
                (
                    request.Page,
                    count
                )
            };
        }
        public async Task<BaseResponse> Create(ProductCategoryParameterCreateRequest request)
        {
            var valid = new CreateCategoryParametersValidator();
            await valid.ValidateAndThrowAsync(request);
            await using var db = new DataContext(_dbContextOptions);
           await db.ProductCategoryParameters.AddAsync(new ProductCategoryParameter()
            {

                Name = request.Name,
                ProductCategoryId = request.ProductCategoryId,
                 Values = request.Values.Select(x=>new ProductCategoryParameterValue()
                 {
                     Value=x,
                     
                 }).ToList(),
            });
            await db.SaveChangesAsync();
            return new BaseResponse(true);
        }
        public async Task<BaseResponse> Update(ProductCategoryParameterUpdateRequest request)
        {
            var valid=new UpdateCategoryParameterValidator();
            await valid.ValidateAndThrowAsync(request);
            await using var db = new DataContext(_dbContextOptions);
            var values = request.Values.Select(x => new ProductCategoryParameterValue()
            {
                Value = x,
                Id = request.Id
            }).ToList();

            var update = await db.ProductCategoryParameters.FirstOrDefaultAsync(x => x.Id == request.Id);
            //выдать ошибку
            if (update == null)
            { return new BaseResponse(false, "Параметров категории с данным айди не существует"); }
                update.Id = request.Id;
                update.ProductCategoryId = request.ProductCategoryId;
                update.Name = request.Name;
               // update.Values = new List<ProductCategoryParameterValue>();
                update.Values= values;

                await db.SaveChangesAsync();

            
            
            return new BaseResponse(true );
        }
        public async Task<BaseResponse> Delete(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var delete = await db.ProductCategoryParameters.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (delete == null) 
                return new BaseResponse( false, "Параметров категории с данным айди не существет" );
            db.ProductCategoryParameters.Remove(delete);
            await db.SaveChangesAsync();
            return new BaseResponse(true);
        }

    }
}
