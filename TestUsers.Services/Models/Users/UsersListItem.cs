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
    public class UsersListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public UsersListItem() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <param name="dateRegister"></param>
        /// <param name="status"></param>
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
