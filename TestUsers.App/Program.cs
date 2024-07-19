// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestUsers.Data;
using TestUsers.Services;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<DataContext>(x =>
    x.UseNpgsql(configuration.GetConnectionString("DataContext")));
        //"Host=localhost;Port=5432;Database=Users;Username=postgres;Password=111111"));

services.AddTransient<UserService>();

var sp = services.BuildServiceProvider();




var userService = sp.GetRequiredService<UserService>();

await userService.GetList(new TestUsers.Services.Models.Users.UsersListRequest() { });
