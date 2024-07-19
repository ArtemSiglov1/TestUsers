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
    public class PageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        public PageRequest(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    /// <summary>
    /// 
    /// </summary>
        public PageRequest() { }
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; } //номер страницы от 1
        /// <summary>
        /// 
        /// </summary>
 public int PageSize { get; set; } // количество записей на странице


    }
}
