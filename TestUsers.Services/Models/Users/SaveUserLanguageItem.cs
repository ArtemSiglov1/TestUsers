using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// сохранить языки пользователя
    /// </summary>
    public class SaveUserLanguageItem
    {
        /// <summary>
        /// идентиф языка
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// дата конца учебы
        /// </summary>
        public DateTime DateLearn { get; set; }
    }
}
