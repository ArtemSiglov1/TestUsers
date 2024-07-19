using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersListResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UsersListItem> Items { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PageResponse Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UsersListResponse() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="page"></param>
        public UsersListResponse(List<UsersListItem> items, PageResponse page)
        {
            Items = items;
            Page = page;
        }
    }
}
