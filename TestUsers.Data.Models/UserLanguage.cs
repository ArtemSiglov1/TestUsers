using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;
namespace TestUsers.Data.Models
{
    /// <summary>
    /// язык пользователя
    /// </summary>
    public class UserLanguage
    {
        /// <summary>
        /// идентиф пользователя
        /// </summary>
     public Guid Id { get; set; }
        /// <summary>   
        /// пользователь кому присвоить
        /// </summary>
     public User User { get; set; }
        /// <summary>
        /// дата окончания изучения
        /// </summary>
     public DateTime DateLearn { get; set; }
        /// <summary>
        /// идентиф пользов
        /// </summary>
     public Guid UserId { get; set; }
        /// <summary>
        /// идентиф языка
        /// </summary>
     public int LanguageId { get; set; }
        /// <summary>
        /// язык 
        /// </summary>
     public Language Language { get; set; }

    }
}
