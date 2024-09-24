using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.News
{
    public class NewsDetailResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public NewsAuthorResponse? Author { get; set; }

        public List<NewsTagResponse> Tags { get; set; } = new List<NewsTagResponse>();



    }
}
