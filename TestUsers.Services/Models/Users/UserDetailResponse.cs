using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Enums;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// класс для детального получения данных о пользователе
    /// </summary>
    public class UserDetailResponse
    {
        public UserDetailResponse() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="email">имэйл</param>
        /// <param name="fullName">полное имя</param>
        /// <param name="dateRegister">дата регистрации</param>
        /// <param name="status">статус</param>
        public UserDetailResponse(Guid id, string email, string fullName, DateTime dateRegister, EnumUserStatus status)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            DateRegister = dateRegister;
            Status = status;
        }
        /// <summary>
        /// идентифик
        /// </summary>

        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;//имейл
  public string FullName { get; set; } = string.Empty; // полное имя
  public DateTime DateRegister { get; set; } // дата регистрации
  public EnumUserStatus Status { get; set; } // статус пользователя
        public override string ToString() { return $"{Id}\t{Email}\t{FullName}\t{DateRegister}\t{Status}"; }
    }
}
