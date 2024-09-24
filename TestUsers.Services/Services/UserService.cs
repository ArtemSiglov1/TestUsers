using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Extensions;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Validators.UserValid;

namespace TestUsers.Services.Services
{
    /// <summary>
    /// класс сервисов для работы пользователя с бд
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// опции работы с данными базы
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
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

            if (request.Status != null)
                query = query.Where(x => x.Status == request.Status);

            var count = await query.CountAsync();

            var users = await query.OrderBy(n => n.FullName)
                .GetPage(request.Page,
                x => new UsersListItem()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                    DateRegister = x.DateRegister,
                    Status = x.Status,
                })
    .ToListAsync();
            var pageResponse = new PageResponse
            ( 
              request.Page,count
            );
            return new UsersListResponse()
            {
                Items = users,
                Page = pageResponse
            };

        }

        /// <summary>
        /// метод возвращающий данные о одном пользователе из бд
        /// </summary>
        /// <param name="userId">идентиф пользователя</param>
        /// <returns></returns>
        public async Task<UserDetailResponse?> GetDetail(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var user = await db.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDetailResponse()
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                DateRegister = u.DateRegister,
                Status = u.Status
            }).FirstOrDefaultAsync();

           return user;
        }
        /// <summary>
        /// метод для создания нового пользователя
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>базовый ответ о том что операция выполнена</returns>
        public async Task<BaseResponse> Create(UserCreateRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.Users.AsQueryable();
            request.Email = request.Email.Trim().ToLower();
            var valid = new CreateUserValidator(query);
            await valid.ValidateAndThrowAsync(request);
            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
            };
            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
            return new BaseResponse (true);
        }

        /// <summary>
        /// метод для редактирования
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>базовый ответ о том что операция выполнена</returns>

        public async Task<BaseResponse> Edit(UserEditRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            
                // получаем первый объект
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                    return new BaseResponse (false, "Пользователь не найден");
                var query = db.Users.AsQueryable();
                var valid = new UserEditValidator(query);
                await valid.ValidateAndThrowAsync(request);
                if (await query
                .Where(u => u.Email == request.Email && u.Id != request.Id)
                .AnyAsync())
                    return new BaseResponse (false, "Имэйл уже занят");
                user.FullName = request.FullName;
                user.Id = request.Id;
                user.Email = request.Email;

                await db.SaveChangesAsync();

                return new BaseResponse (true);

            
        }

        /// <summary>
        /// метод для удаления пользователя из бд
        /// </summary>
        /// <param name="userId">идентиф пользователя</param>
        public async Task Delete(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);
           var del= await db.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (del == null) return;
            db.Users.Remove(del);
            await db.SaveChangesAsync();
        }
    }
}
