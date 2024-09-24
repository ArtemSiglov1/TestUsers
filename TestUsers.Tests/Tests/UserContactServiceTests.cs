using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Data;
using TestUsers.Services.Services;
using TestUsers.Services.Models.Users;

namespace TestUsers.Tests.Tests
{
    public class UserContactServiceTests
    {
        private readonly UserContactsService _service;
        private readonly User user;
        public UserContactServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            user = InitToTable.InitUser();
            db.Users.Add(user);
            db.SaveChangesAsync();
            _service = serviceProvider.GetRequiredService<UserContactsService>();
        }
        [Fact]
        public async Task SuccessGetConact()
        {
            var result = await _service.GetContacts(user.Id);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task SuccessSaveCotacts()
        {
            //var initContacts = ;
            var result = await _service.GetContacts(user.Id);
            var tg=result.Last();
            await _service.SaveContacts(new Services.Models.Users.UserContactSaveRequest
            {
                UserId = user.Id,
                Contacts = [new Services.Models.Users.UserContactItem
                {
                    Id = tg.Id,
                   Name="tg",
                   Value="+37377804696"
                } ,
                new Services.Models.Users.UserContactItem{
                    
                    Name="Vk",
                    Value="pomoika"
                }
                ]
            });
             result = await _service.GetContacts(user.Id);
           Assert.Equal(2,result.Count);
            Assert.Equal(tg.Id, result[0].Id);
            Assert.True(result.Any(x => x.Name == "tg"), "");
            Assert.True(result.Any(x => x.Value == "+37377804696"), "");
            Assert.True(result.Any(x => x.Name == "Vk"), "");
            Assert.True(result.Any(x => x.Value == "pomoika"), "");
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task NotSuccessSaveCotactsValidName()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
               () => _service.SaveContacts(new UserContactSaveRequest
            {
                UserId = user.Id,
                Contacts = [new UserContactItem
                {
                   Value="+37377804696"
                }
                ]
            }));
        }
        [Fact]
        public async Task NotSuccessSaveCotactsValidValue()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
               () => _service.SaveContacts(new Services.Models.Users.UserContactSaveRequest
               {
                   UserId = user.Id,
                   Contacts = [new UserContactItem
                {
                       Name="tg",
                }
                ]
               }));
        }
    }
}
