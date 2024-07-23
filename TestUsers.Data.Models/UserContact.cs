using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Data.Models
{
    /// <summary>
    /// контакт пользователя
    /// </summary>
    public class UserContact
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// имя контакта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// номер или юз
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// пользователь кому присвоить 
        /// </summary>
       public  User User { get; set; }
        /// <summary>
        /// идентиф пользователя
        /// </summary>
        public Guid UserId { get; set; }

    }
}
