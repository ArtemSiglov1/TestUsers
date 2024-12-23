﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// лист пользователей ответов
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
        /// конструктор по умолчанию
        /// </summary>
        public UsersListResponse() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="items">предмет</param>
        /// <param name="page">страница</param>
        public UsersListResponse(List<UsersListItem> items, PageResponse page)
        {
            Items = items;
            Page = page;
        }
    }
}
