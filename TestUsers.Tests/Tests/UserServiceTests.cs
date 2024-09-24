using Microsoft.Extensions.DependencyInjection;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Services;

namespace TestUsers.Tests.Tests
{
    public class UserServiceTests
    {
        readonly private UserService _service;
        User User { get; set; }
        public UserServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            User = InitToTable.InitUser();
            db.Users.Add(User);
            db.SaveChangesAsync();
            _service = serviceProvider.GetRequiredService<UserService>();

        }
        [Fact(DisplayName = "получение содержимого бд успешный кейс")]
        public async Task SuccessGetList()
        {
            var result = await _service.GetList(new UsersListRequest() { Search = "t", Page = new Services.Models.PageRequest { Page = 1, PageSize = 2 } });
            Assert.NotNull(result);
            Assert.True(result.Items.Any(x => x.FullName.Contains('t')),"");
            var result1 = await _service.GetList(new UsersListRequest
            {
                Search = "ldldldlldldddd",
                Page = new Services.Models.PageRequest { Page = 1, PageSize = 2 }
            });

            Assert.Empty(result1.Items);
            Assert.True(result.Items.Any(x => x.Status == Data.Enums.EnumUserStatus.Active), "");
        }


        [Fact]
        public async Task NotSuccessSearchStatus()
        {
            var result = await _service.GetList(new UsersListRequest 
            {
                Search = "t", 
                Status = Data.Enums.EnumUserStatus.NotConfirmed, 
                Page = new Services.Models.PageRequest 
                { 
                    Page = 1, 
                    PageSize = 2 
                } 
            });
            Assert.Empty( result.Items);
        }

        [Fact]
        public async Task SuccessGetDetail()
        {
            var result = await _service.GetDetail(User.Id);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task SuccessCreateUser()
        {
            string email = "artemsiglov@gmail.com";
            string fullName = "Artem";
            await _service.Create(new UserCreateRequest {
                FullName =fullName ,
                Email = email
            });
            var result = await _service.GetList(new UsersListRequest() { 
                Search = fullName,
                Page = new Services.Models.PageRequest 
                {
                    Page = 1, 
                    PageSize = 2 
                } 
            });
            Assert.True(result.Items.Any(x=>x.FullName== fullName),"");
            Assert.NotEmpty(result.Items);
        }
        [Fact]
        public async Task NotSuccessCreateUserValidFullName()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.
                 Create(new UserCreateRequest
                 {
                     FullName = "",
                     Email = "artemsiglov@gmail.com"
                 }));

        }
        [Fact]
        public async Task NotSuccessCreateUserValidEmail()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.
                 Create(new UserCreateRequest
                 {
                     FullName = "Artem",
                     Email = "dddd"
                 }));

        }
        [Fact]
        public async Task SuccessEditUser()
        {
            var edit = new UserEditRequest
            {
                Id = User.Id,
                FullName = "Artem",
                Email = "artemsiglov@gmail.com"
            };
            await _service.Edit(edit);
            var res = await _service.GetDetail(User.Id);
            Assert.NotNull(res);
            Assert.Equal(edit.FullName, res.FullName);
            Assert.Equal(edit.Email, res.Email);
        }
        [Fact]
        public async Task NotSuccessEditUserValidFullName()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.
                 Edit(new UserEditRequest
                 {
                     Id = User.Id,
                     FullName = "",
                     Email = "artemsiglov@gmail.com"
                 }));

        }
        [Fact]
        public async Task NotSuccessEditUserValidEmail()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.
                 Edit(new UserEditRequest
                 {
                     Id = User.Id,
                     FullName = "Artem",
                     Email = "dddd"
                 }));
        }


        [Fact]
        public async Task NotSuccessEditUserValidEmailEmpty()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                 () => _service.
                 Edit(new UserEditRequest
                 {
                     Id = User.Id,
                     FullName = "Artem",
                     Email = ""
                 }));
        }

        [Fact]
        public async Task SuccessDeleteUser()
        {
            await _service.Delete(User.Id);
            var result = await _service.GetDetail(User.Id);
            Assert.Null(result);
        }

    }
}