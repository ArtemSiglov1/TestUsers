using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Data;
using TestUsers.Services.Services;
using TestUsers.Services.Models.News;

namespace TestUsers.Tests.Tests
{
    public class NewsServiceTests
    {
        private NewsService _service;
        User user;
        public NewsServiceTests()
        {
            var serviceProvider = Config.ServiceProvider;
            using var db = serviceProvider.GetRequiredService<DataContext>();
            user = InitToTable.InitUser();
            db.Users.Add(user);
            db.SaveChangesAsync();
            _service = serviceProvider.GetRequiredService<NewsService>();
        }
        [Fact]
        public async Task SuccessGetList()
        {
            var result = await _service.GetList(new Services.Models.News.NewsListRequest()
            {
                Page = new Services.Models.PageRequest()
                {
                    Page = 1,
                    PageSize = 2,
                },
                TagId = 1
            });
            Assert.NotNull(result);
            
        }
        [Fact]
        public async Task SuccessGetDetail()
        {
            var result = await _service.GetDetail(1);
            Assert.NotNull(result);
            Assert.Equal(1,result.Id);
        }
        [Fact]
        public async Task SuccessCreateNews()
        {
            await _service.Create(new Services.Models.News.NewsCreateRequest
            {
                Description = "Test2",
                AuthorId = 1,
                Tags = "qqq,qq,www,eee",
                Title = "Test2"
            });
            var result = await _service.GetList(new Services.Models.News.NewsListRequest()
            {
                Page = new Services.Models.PageRequest()
                {
                    Page = 1,
                    PageSize = 2,
                },
                Search="Test2"
            });
            Assert.NotNull(result);
            Assert.True(result.Items.Any(x=>x.Title=="Test2"),"");

        }
        [Fact]
        public async Task NotSuccessCreateNewsValidTitleSize()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
                () => _service.Create(new Services.Models.News.NewsCreateRequest
                {
                    Description = "Test2",
                    AuthorId = 1,
                    Tags = "qqq,qqq,www,eee",
                    Title = "Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2"
                }));
        }
        [Fact]
        public async Task NotSuccessCreateNewsValidAuthorId()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
               () => _service.Create(new Services.Models.News.NewsCreateRequest
               {
                   Description = "Test2",
                   AuthorId = -1,
                   Tags = "qqq,qqq,www,eee",
                   Title = "Test2"

               }));
        }

        [Fact]
        public async Task NotSuccessCreateNewsValidDescription()
        {
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(
               () => _service.Create(new Services.Models.News.NewsCreateRequest
               {
                   Description = "Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2Test2",
                   AuthorId = 1,
                   Tags = "qqq,qqq,www,eee",
                   Title = "Test2"

               }));
        }
        [Fact]
        public async Task SuccessEditNews()
        {
            var request = new NewsEditRequest
            {
                Id = 1,
                AuthorId = 1,
                Description = "p",
                Title = "f",
                Tags = "f,f,g,h"
            };

            await _service.Edit(request);


            var updatedNews = await _service.GetDetail(1);

            Assert.NotNull(updatedNews);
            Assert.Equal("f", updatedNews.Title);
            Assert.Equal("p", updatedNews.Description);
            Assert.Equal("f", updatedNews.Tags[0].Name);
            Assert.Equal("g", updatedNews.Tags[1].Name);
            Assert.Equal( "h" , updatedNews.Tags.Last().Name);

        }
    }
}
