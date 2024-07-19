using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Security.Cryptography.X509Certificates;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services
{
    /// <summary>
    /// класс сервисов для работы пользователя с бд
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public UserService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// метод возвращающий содержимое базы данных 
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>список пользователей</returns>
        public async Task<UsersListResponse> GetList(UsersListRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);

            
            
            
            var query = db.Users.AsQueryable();
            




            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(x => x.FullName.Contains(request.Search));
            
            
            
            
            if (request.Status!=null)
                query =  query.Where(x => x.Status== request.Status);








            var count = await query.CountAsync();







            var users = await query.OrderBy(n=>n)
                .Select(x => new UsersListItem()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                    DateRegister = x.DateRegister,
                    Status = x.Status,
                }).Skip((request.Page.Page - 1) * request.Page.PageSize)
    .Take(request.Page.PageSize)
    .ToListAsync();

            var pageResponse = new PageResponse
            {
                
                Count= count,
                Page = request.Page.Page,
                PageSize = request.Page.PageSize
            };

            //var items =  query.Skip((request.Page.Page - 1) * request.Page.PageSize).Take(request.Page.PageSize);

            return new UsersListResponse()
            {
                Items = users,
                Page=pageResponse
            };

        }
        /// <summary>
        /// метод возвращающий данные о одном пользователе из бд
        /// </summary>
        /// <param name="userId">идентиф пользователя</param>
        /// <returns></returns>
        public async Task<UserDetailResponse> GetDetail(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var users = await db.Users.Where(u => u.Id == userId).Select(u=>new UserDetailResponse()
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                DateRegister = u.DateRegister,
                Status = u.Status
            }).FirstOrDefaultAsync();

            return users;
        }






        /// <summary>
        /// метод для создания нового пользователя
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>базовый ответ о том что операция выполнена</returns>

        public async Task<BaseResponse> Create(UserCreateRequest request)
            {
                await using var db = new DataContext(_dbContextOptions);

                var newUser = new User
                {

                    FullName = request.FullName,
                    Email = request.Email,
                };

               await db.Users.AddAsync(newUser);

                await db.SaveChangesAsync();

                return new BaseResponse { IsSuccess = true, ErrorMessage = "" };
            }



        /// <summary>
        /// метод для редактирования
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>базовый ответ о том что операция выполнена</returns>

        public async Task<BaseResponse> Edit(UserEditRequest request) {
            using (DataContext db = new DataContext())
            {
                // получаем первый объект
                var user = await db.Users.FirstOrDefaultAsync();
                if (user != null)
                {
                    user.FullName = request.FullName;
                    user.Id = request.Id;
                    //обновляем объект
                    //db.Users.Update(user);
                   await db.SaveChangesAsync();
                }
                return new BaseResponse { IsSuccess = true, ErrorMessage = "Пользователь изменен" };

            }
        }





        /// <summary>
        /// метод для удаления пользователя из бд
        /// </summary>
        /// <param name="userId">идентиф пользователя</param>
        public async void Delete(Guid userId)
        {
            using(
        DataContext db = new DataContext()
            ){
                // обновляем только объекты, у которых имя Bob
                await db.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
            }
        }
    }
} 
