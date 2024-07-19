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
    public class UserDetailResponse
    {
        /// <summary>
        /// 
        /// </summary>
            public UserDetailResponse() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <param name="dateRegister"></param>
        /// <param name="status"></param>
        public UserDetailResponse(Guid id, string email, string fullName, DateTime dateRegister, EnumUserStatus status)
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
 public  string Email { get; set; }//имейл
  public string FullName { get; set; } // полное имя
  public DateTime DateRegister { get; set; } // дата регистрации
  public EnumUserStatus Status { get; set; } // статус пользователя
    }
}
