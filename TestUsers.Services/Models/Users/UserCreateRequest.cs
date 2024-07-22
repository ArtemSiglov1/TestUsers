﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// создание пользователем запроса
    /// </summary>
    public class UserCreateRequest
    {
        /// <summary>
        /// конструктор по умолчанию 
        /// </summary>
        public UserCreateRequest() { }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="email">имейл</param>
        /// <param name="fullName">полное имя</param>
        public UserCreateRequest(string email, string fullName)
        {
            Email = email;
            FullName = fullName;
        }
        /// <summary>
        /// имейл
        /// </summary>
        public string Email { get; set; } //имейл
        /// <summary>
        /// полное имя
        /// </summary>
        public string FullName { get; set; } // полное имя
    }
}
