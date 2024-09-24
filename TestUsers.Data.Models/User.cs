using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestUsers.Data.Enums;

namespace TestUsers.Data.Models
{
    /// <summary>
    /// пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// имейл
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// полное имя
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// хэш пароля
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        /// <summary>
        /// дата регистрации
        /// </summary>
        public DateTime DateRegister { get; set; }
        /// <summary>
        /// статус пользователя
        /// </summary>
        public EnumUserStatus Status { get; set; }
        /// <summary>
        /// контакты пользователя
        /// </summary>
        public List<UserContact>?Contacts { get; set; }
       /// <summary>
       /// языки пользователя
       /// </summary>
        public  List<UserLanguage>? UserLanguages { get; set; }
        public List<News> News { get; set; }= new List<News>();

    }
}