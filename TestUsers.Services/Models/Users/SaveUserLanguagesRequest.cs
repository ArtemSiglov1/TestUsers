using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// сохранение языков пользователя 
    /// </summary>
    public class SaveUserLanguagesRequest
    {
        /// <summary>
        /// идентиф пользов
        /// </summary>
        public Guid UserId{ get; set; }
        /// <summary>
        /// список языков знаемых пользователем
        /// </summary>
        public List<SaveUserLanguageItem> Items { get; set; }

    }
}
