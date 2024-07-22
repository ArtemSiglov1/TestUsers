using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Enums;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// лист пользователей
    /// </summary>
    public class UsersListItem
    {
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public UsersListItem() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="id">идентиф</param>
        /// <param name="email">имейл</param>
        /// <param name="fullName">полное имя</param>
        /// <param name="dateRegister">дата регистрации</param>
        /// <param name="status">статус</param>
        public UsersListItem(Guid id, string email, string fullName, DateTime dateRegister, EnumUserStatus status)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            DateRegister = dateRegister;
            Status = status;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateRegister { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumUserStatus Status { get; set; }

    }
}
