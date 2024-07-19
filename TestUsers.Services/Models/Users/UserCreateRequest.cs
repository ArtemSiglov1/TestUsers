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
    public class UserCreateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public UserCreateRequest() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        public UserCreateRequest(string email, string fullName)
        {
            Email = email;
            FullName = fullName;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } //имейл
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; } // полное имя
    }
}
