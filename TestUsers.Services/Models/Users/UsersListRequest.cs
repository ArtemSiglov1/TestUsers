using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Enums;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// лист пользователей вопрос
    /// </summary>
    public class UsersListRequest
    {
        /// <summary>
        /// конструктор по умолчанию 
        /// </summary>
        public UsersListRequest() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="search">поиск</param>
        /// <param name="status">статус</param>
        /// <param name="page">страница</param>
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
