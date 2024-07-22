using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services
{
    public class UserLanguageService
    {
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
        public async Task<List<UserLanguageItemResponse>> GetList(int userId) {
            await using var db = new DataContext(_dbContextOptions);


            var userLanguages = await db.UsersLanguage.OrderBy(n => n.DateLearn)
                .Select(x => new UserLanguageItemResponse()
                {
                   DateLearn=x.DateLearn,
                  Code=x.Language.Code,
                  LanguageId=x.LanguageId,
                  Name=x.Language.Name
                })
    .ToListAsync();


            return userLanguages;
        }
//добавить один язык к пользователю
public BaseResponse AddLanguageToUser(AddLanguageToUser request) {
        
        }
//сохранить список языков пользователя
public BaseResponse SaveUserLanguages(SaveUserLanguagesRequest request) { }
    }
}
