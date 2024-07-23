using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// сервис для добавления языка пользователю
    /// </summary>
    public class AddLanguageToUser
    {
        /// <summary>
        /// идентиф пользов
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// идентиф языка
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// дата конца обучения языку
        /// </summary>
        public DateTime DateLearn { get; set; }
    }
}
