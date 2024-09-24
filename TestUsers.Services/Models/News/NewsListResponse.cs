using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.News
{
    public class NewsListResponse
    {
        public List<NewsListItem> Items { get; set; } = new List<NewsListItem>(); //новости 

        public PageResponse? Page { get; set; } //данные для постранички



    }
}
