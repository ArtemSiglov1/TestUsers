using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestUsers.Data;
using TestUsers.Data.Migrations;
using TestUsers.Data.Models;
using TestUsers.Services.Extensions;
using TestUsers.Services.Models;
using TestUsers.Services.Models.News;
using TestUsers.Services.Validators.NewsValid;

namespace TestUsers.Services.Services
{
    public class NewsService
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public NewsService(DbContextOptions<DataContext> dbContextOptions) =>
            _dbContextOptions = dbContextOptions;
        public async Task<NewsListResponse?> GetList(NewsListRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.News.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(news => news.Title.Contains(request.Search)
                    || news.Author.FullName.Contains(request.Search)
                    || news.Tags.Any(tag => tag.NewsTag.Name.Contains(request.Search)));

            }
            //необязателные фильиры плюс скип и тэк в метод расширения
            var count = query.Count();
            var items = await query
                .Where(news => news.Tags.Any(tag => tag.NewsTagId == request.TagId))
                .GetPage(request.Page,
                news => new NewsListItem
                {
                    Id = news.Id,
                    Title = news.Title,
                    DateCreated = news.DateCreated,
                    AuthorId = news.AuthorId
                })
                .ToListAsync();

            return new NewsListResponse
            {
                Items = items,
                Page = new PageResponse(request.Page, count)
            };
        }

        public async Task<NewsDetailResponse?> GetDetail(int newsId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var news = await db.News
                .Select(x => new NewsDetailResponse()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    DateCreated = x.DateCreated,
                    Author = new NewsAuthorResponse
                    {
                        Id = x.Author.Id,
                        Name = x.Author.FullName
                    },
                    Tags = x.Tags.Select(tag => new NewsTagResponse
                    {
                        Id = tag.NewsTag.Id,
                        Name = tag.NewsTag.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync(n => n.Id == newsId);

            return news;
        }

        public async Task<BaseResponse> Create(NewsCreateRequest request)
        {
            if (request == null)
            {
                return new BaseResponse(false, "Запрос не может быть пустым");
            }
            var validator = new CreateNewsValidator();
            await validator.ValidateAndThrowAsync(request);
            var news = new News
            {
                Title = request.Title,
                Description = request.Description,
                AuthorId = request.AuthorId,
                DateCreated = DateTime.Now
            };
            await using var db = new DataContext(_dbContextOptions);

            if (!string.IsNullOrEmpty(request.Tags))
            {
                var tags = request.Tags.Split(',')
                    .Select(tag => tag.Trim())
                    .Distinct()
                    .ToList();

                var tag = await db.NewsTags.Where(t => tags
                .Any(x => x == t.Name) == false)
                .Select(x => new NewsTag { Name = x.Name })
                .ToListAsync();
                var relation = new List<NewsTagsRelation>();
                if (tag == null)
                {
                    return new BaseResponse();
                }
                await db.NewsTags.AddRangeAsync(tag);
                foreach (var item in tag)
                {
                    relation.Add(new NewsTagsRelation { NewsTag = item });
                }

            }
            await db.News.AddAsync(news);
            await db.SaveChangesAsync();
            return new BaseResponse(true);

        }

        public async Task<BaseResponse> Edit(NewsEditRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);

            if (request == null)
                return new BaseResponse(false, "Запрос не может быть пустым");


            var news = await db.News.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (news == null)
                return new BaseResponse(false, "Новость не найдена");

            var tags = request.Tags.Split(',')
                           .Select(tag => tag.Trim())
                           .Distinct()
                           .ToList();
            news.Title = request.Title;
            news.Description = request.Description;
            news.AuthorId = request.AuthorId;

            //for (var i = 0; i < tags.Count; i++)
            //{
            //    for (var j = 0; j < tags.Count; j++)
            //    { 
            //        if (tags[i] == tags[j] && i != j)
            //        {
            //            tags.RemoveAt(i);
            //        };
            //    }
            //}

            var newsTags =await db.News.Select(x => x.Tags).FirstOrDefaultAsync();
            if (newsTags == null) { return new BaseResponse(false, "Тэгов нет"); }
            var tagsToRemove= newsTags.Where(t => !tags.Contains(t.NewsTag.Name)).ToList();
            foreach (var t in tagsToRemove)
                news.Tags.Remove(t);

                db.NewsTagsRelations.RemoveRange(tagsToRemove);
            var listTags = await db.NewsTags.ToListAsync();
            var existingTags = new List<NewsTag>();

            foreach (var tagName in tags)

                if (!news.Tags.Any(t => t.NewsTag.Name == tagName))
                {
                    var existingTag = listTags.FirstOrDefault(t => t.Name == tagName);
                    if (existingTag == null)
                    {
                        existingTag = new NewsTag { Name = tagName };
                        existingTags.Add(existingTag);
                    }

                    news.Tags.Add(new NewsTagsRelation
                    {
                        NewsTag = existingTag,
                        News = news
                    });
                }

            if (existingTags.Count != 0)
                await db.NewsTags.AddRangeAsync(existingTags);

            await db.SaveChangesAsync();

            return new BaseResponse(true);
        }
    }

}
