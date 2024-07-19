using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        public PageResponse(int page, int pageSize, int count)
        {
            Page = page;
            PageSize = pageSize;
            Count = count;
            
        }
        /// <summary>
        /// 
        /// </summary>
        public PageResponse() { }
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; } //номер страницы
        /// <summary>
        /// 
        /// </summary>
 public int PageSize { get; set; }// количество записей на странице
        /// <summary>
        /// 
        /// </summary>
  public int Count { get; set; } // общее количество записей всего
        
    }
}
