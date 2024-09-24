
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using TestUsers.Data;
using TestUsers.Services.Services;

namespace TestUsers.Tests
{
    public class Config
    {
        // static DbContextOptions<DataContext> _dbContextOptionsPostgress { get; set; }

        // private static readonly IConfiguration configuration=new ConfigurationBuilder()
        //.AddJsonFile("appsettings.json").Build();

        private static IServiceProvider? _serviceProvider = null;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider != null)
                {
                    return _serviceProvider;
                }
                return _serviceProvider = InitServiceProvider();

            }
        }


        static IServiceProvider InitServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DataContext>((sp, options) =>
                {
                    options.UseInMemoryDatabase("Test").UseInternalServiceProvider(sp);
                });

            services.AddTransient<UserService>(); 
            services.AddTransient<UserLanguageService>();
            services.AddTransient<UserContactsService>();
            services.AddTransient<NewsService>();
            services.AddTransient<ProductCategoryService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ProductCategoryParametersService>();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }


        public T GetRequiredService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
