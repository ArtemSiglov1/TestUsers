using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Validators.UserLanguageValid;

namespace TestUsers.Services.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLanguageService
    {
        /// <summary>
        /// 
        /// </summary>
        readonly private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public UserLanguageService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<List<UserLanguageItemResponse>> GetList(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var userLanguages = await db.UsersLanguage
                .Where(u => u.UserId == userId)
                .OrderBy(n => n.DateLearn)
                .Select(x => new UserLanguageItemResponse
                {
                    DateLearn = x.DateLearn,
                    Code = x.Language!.Code,
                    LanguageId = x.LanguageId,
                    Name = x.Language.Name
                })
                .ToListAsync();

            return userLanguages;
        }

        public async Task<BaseResponse> AddLanguagesToUser(AddLanguageToUser request)
        {
            await using var db = new DataContext(_dbContextOptions);

            var valid = new AddLanguageToUserValidator(db.UsersLanguage.Where(x => x.UserId == request.UserId).AsQueryable());
            await valid.ValidateAndThrowAsync(request);
            var newUserLanguage = new UserLanguage
            {
                UserId = request.UserId,
                DateLearn = request.DateLearn,
                LanguageId = request.LanguageId,     
                
            };
            await db.UsersLanguage.AddAsync(newUserLanguage);
            await db.SaveChangesAsync();

            return new BaseResponse(true);
        }

        //сохранить список языков пользователя
        public async Task<BaseResponse> SaveUserLanguages(SaveUserLanguagesRequest request)
        {
            var validator = new SaveUserLanguageValidator();
            await validator.ValidateAndThrowAsync(request);

            await using var db = new DataContext(_dbContextOptions);

            // Получаем все возможные LanguageId из базы данных
            var languageIds = await db.Language.Select(x => x.Id).ToListAsync();

            // Получаем все языки пользователя
            var existingUserLanguages = await db.UsersLanguage
                .Where(x => x.UserId == request.UserId)
                .ToListAsync();

            // Фильтруем запрос по допустимым LanguageId
            var requestLanguages = request.Items
                .Where(c => languageIds.Contains(c.LanguageId))
                .ToList();

            // Извлекаем ID языков из запроса
            var requestLanguageIds = requestLanguages.Select(c => c.LanguageId).ToList();

            if (requestLanguageIds.Count == 0)
            {
                return new BaseResponse(false, "Языка с данным ID не существует.");
            }

            // Проверяем, есть ли языки из запроса, которые уже присутствуют у пользователя
            var duplicateLanguages = existingUserLanguages
                .Where(x => requestLanguageIds.Contains(x.LanguageId))
                .ToList();

            if (duplicateLanguages.Count != 0)
            {
                return new BaseResponse(false, "Вы передали уже существующий у этого пользователя язык");
            }
                var languagesToRemove = existingUserLanguages
                .Where(x => !requestLanguageIds.Contains(x.LanguageId))
                .ToList();

            db.UsersLanguage.RemoveRange(languagesToRemove);            
            var languagesToAdd = request.Items
                .Where(x => !existingUserLanguages.Any(e => e.LanguageId == x.LanguageId))
                .Select(x => new UserLanguage
                {
                    UserId = request.UserId,
                    LanguageId = x.LanguageId,
                    DateLearn = x.DateLearn,
                })
                .ToList();

            await db.UsersLanguage.AddRangeAsync(languagesToAdd);

            await db.SaveChangesAsync();
            return new BaseResponse(true);
        }
    }
}
