using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// запрос на редактир пользов
    /// </summary>
    public class UserEditRequest
    {
        public UserEditRequest() { }
        /// конструктор с параметрами
        /// </summary>
        /// <param name="id">идентиф</param>
        /// <param name="fullName">полное имя</param>
        public UserEditRequest(Guid id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
        /// <summary>
        /// идентиф
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// полное имя
        /// </summary>
  public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// имейл
        /// </summary>

        public string Email { get; set; }= string.Empty;
    }
}
