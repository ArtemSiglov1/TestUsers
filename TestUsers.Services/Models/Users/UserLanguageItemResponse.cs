using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// язык пользователя добавить
    /// </summary>
    public class UserLanguageItemResponse
    {
        /// <summary>
        /// идентиф
        /// </summary>
       public int LanguageId { get; set; }
        /// <summary>
        /// код
        /// </summary>
       public string Code { get; set; }  //ru, en
        /// <summary>
        /// название
        /// </summary>
       public string Name { get; set; }//Russian, English
        /// <summary>
        /// дата конца обучения
        /// </summary>
       public DateTime DateLearn { get; set; }
    }
}
