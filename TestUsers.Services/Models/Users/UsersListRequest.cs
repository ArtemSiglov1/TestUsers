using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Enums;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersListRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public UsersListRequest() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="status"></param>
        /// <param name="page"></param>
        public UsersListRequest(string search, EnumUserStatus? status, PageRequest page)
        {
            Search = search;
            Status = status;
            Page = page;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumUserStatus? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public  PageRequest Page { get; set; }
      
    }
}
