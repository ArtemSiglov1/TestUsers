using Microsoft.Extensions.DependencyInjection;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Services;

namespace TestUsers.Tests.Tests
{
    public class UserLanguageServiceTests
    {
        readonly private UserLanguageService _service;
        private User User { get; set; }
        public UserLanguageServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            db.Language.Add(new Language() { Code = "36", Name = "Russian" });
            db.Language.Add(new Language() { Code = "31", Name = "Spanish" });

            User = InitToTable.InitUser();

            db.Users.Add(User);
            db.SaveChangesAsync();
            _service = serviceProvider.GetRequiredService<UserLanguageService>();
        }
        [Fact]
        public async Task SuccessGetList()
        {
            var result = await _service.GetList(User.Id);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task SuccessAddLanguagesToUser()
        {
            await _service.AddLanguagesToUser(new Services.Models.Users.AddLanguageToUser
            {
                DateLearn = DateTime.UtcNow,
                LanguageId = 2,
                UserId = User.Id
            });
            var result = await _service.GetList(User.Id);
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[1].LanguageId);
        }
        [Fact]
        public async Task SuccessAddLanguagesToUserValidLanguageId()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.AddLanguagesToUser(new Services.Models.Users.AddLanguageToUser
                 {
                     DateLearn = DateTime.UtcNow,
                     LanguageId = 1,
                     UserId = User.Id
                 }));
        }
        [Fact]
        public async Task SuccessSaveUserLanguage()
        {
            await _service.SaveUserLanguages(new SaveUserLanguagesRequest
            {
                UserId = User.Id,
                Items = [
                    new SaveUserLanguageItem{
                DateLearn= DateTime.UtcNow,
                LanguageId=2
                },
                    new SaveUserLanguageItem{
                        DateLearn=DateTime.UtcNow,
                        LanguageId=3
                    }
                ]

            });
            var result = await _service.GetList(User.Id);
            Assert.Equal(2,result.Count);
            Assert.Contains(result, x => x.LanguageId == 2);
            Assert.Contains(result, x => x.LanguageId == 3);
            Assert.DoesNotContain(result, x => x.LanguageId == 1);

        }
        [Fact]
        public async Task NotSuccessSaveUserLanguageValid()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                () => _service.SaveUserLanguages(new Services.Models.Users.SaveUserLanguagesRequest
                {
                    UserId = User.Id,
                }));

        }
        [Fact]
        public async Task NotSuccessSaveUserLanguageValidItems()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                () => _service.SaveUserLanguages(new Services.Models.Users.SaveUserLanguagesRequest
                {
                    UserId = User.Id,
                    Items =
                    [
                        new SaveUserLanguageItem {
                            DateLearn = DateTime.UtcNow,

                        }
                    ]
                })
            );
        }
    }
}
