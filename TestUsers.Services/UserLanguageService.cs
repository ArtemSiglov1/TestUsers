using Microsoft.EntityFrameworkCore;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLanguageService
    {
        /// <summary>
        /// 
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public UserLanguageService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        //получить список изучаемых языков пользователя
        public async Task<List<UserLanguageItemResponse>> GetList(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);


            var userLanguages = await db.UsersLanguage.Where(u => u.UserId == userId).OrderBy(n => n.DateLearn)
                .Select(x => new UserLanguageItemResponse()
                {
                    DateLearn = x.DateLearn,
                    Code = x.Language.Code,
                    LanguageId = x.LanguageId,
                    Name = x.Language.Name
                })
    .ToListAsync();


            return userLanguages;
        }
        //добавить один язык к пользователю
        public async Task<BaseResponse> AddLanguagesToUser(AddLanguageToUser request)
        {
            await using var db = new DataContext(_dbContextOptions);

            var query = db.UsersLanguage.AsQueryable();
            var DbLanguage = await query.Where(u => u.Id == request.UserId).ToListAsync();
            if (request.DateLearn.Year < DateTime.Now.Year - 100)
                return new BaseResponse { IsSuccess = true, ErrorMessage = "Вы указали не верный год" };
            if (request.LanguageId == 0)
                return new BaseResponse { IsSuccess = true, ErrorMessage = "Вы не указали идентификатор языка" };
            if (DbLanguage.Any(x => x.LanguageId == request.LanguageId))
                return new BaseResponse { IsSuccess = true, ErrorMessage = "Человек уже знает данный язык" };
            var newUser = new UserLanguage
            {
                UserId = request.UserId,
                DateLearn = request.DateLearn,
                LanguageId = request.LanguageId,
            };
            await db.UsersLanguage.AddAsync(newUser);

            await db.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        //сохранить список языков пользователя
        public async Task<BaseResponse> SaveUserLanguages(SaveUserLanguagesRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var dbData = await db.UsersLanguage.ToListAsync();
            var languageItem = dbData.Select(x => new SaveUserLanguageItem() { LanguageId = x.LanguageId, DateLearn = x.DateLearn }).ToList();

            foreach (var language in request.Items)
            {
                var languages = languageItem?.Select(x => new UserLanguage { LanguageId = language.LanguageId, DateLearn = language.DateLearn }).FirstOrDefault();


                await db.UsersLanguage.AddAsync(languages);

            }
            return new BaseResponse { IsSuccess = true };

        }
    }
}
