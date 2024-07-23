using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// сщхранение контактов пользователя
    /// </summary>
    internal class UserContactSaveRequest
    {
        /// <summary>
        /// конструктор
        /// </summary>
        public UserContactSaveRequest()
        {
        }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="userId">идентиф пользователя</param>
        /// <param name="contacts">список контактов</param>
        public UserContactSaveRequest(Guid userId, List<UserContactItem> contacts)
        {
            UserId = userId;
            Contacts = contacts;
        }
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// список контактов
        /// </summary>
        public List<UserContactItem>? Contacts { get;set; }
    }
}
