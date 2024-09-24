using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestUsers.Data;
using TestUsers.Services.Models.Users;
using TestUsers.Services.Services;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<DataContext>(x =>
    x.UseNpgsql(configuration.GetConnectionString("DataContext")));
        //"Host=localhost;Port=5432;Database=UsersLunguage;Username=postgres;Password=111111";

//services.AddTransient<UserService>();
services.AddTransient<UserLanguageService>();

var sp = services.BuildServiceProvider();




//var userService = sp.GetRequiredService<UserService>();
var userLanguage = sp.GetRequiredService<UserLanguageService>();
Console.WriteLine("Create");
//await userService.Create(new UserCreateRequest()
//{
//    FullName = "Siglov Artem V",
//    Email = "artemsiglov@gmail.com"
//});
//await userService.Create(new UserCreateRequest()
//{
//    FullName = "Siglov Artem f",
//    Email = "artemsiglov5@gmail.com"
//});
//Console.WriteLine("Detail Output ");
//UsersListResponse response =await userService.GetList(new UsersListRequest("Siglov",0,new PageRequest(2,3)));

//Console.WriteLine(  userService.GetDetail(Guid.Parse("4b72acb7-8c62-4131-b3d1-7afe15251578")).Result);
//await userService.Edit(new UserEditRequest(Guid.Parse("b57ab894-bfa6-4a4c-9b89-28bc6b4c7e53"),"Kirill Olegovich", "KirillOlegovich@gmail.com"));
//Console.WriteLine(userService.GetDetail(Guid.Parse("b57ab894-bfa6-4a4c-9b89-28bc6b4c7e53")).Result);
//Console.WriteLine("Output");
//// Обработка полученного ответа
//foreach (var user in response.Items)
//{
//    Console.WriteLine($"User: {user.FullName}, Email: {user.Email}, Status: {user.Status}");
//}
//userService.Delete(Guid.Parse("0b121cfd-2a5f-4a6e-a901-f01913da54e3"));
var res = await userLanguage.AddLanguagesToUser(new AddLanguageToUser() { UserId = Guid.Parse("9e4eaec3-8d40-4cd0-bad9-658f719e03d9"), });
Console.ReadLine();

