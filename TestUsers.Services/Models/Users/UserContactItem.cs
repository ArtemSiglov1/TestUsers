using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// контакты пользователя добавить
    /// </summary>
    internal class UserContactItem
    {
        /// <summary>
        /// конструктор
        /// </summary>
        public UserContactItem() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="id">идентиф</param>
        /// <param name="name">название</param>
        /// <param name="value">значение</param>
        public UserContactItem(Guid? id, string name, string value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
        /// <summary>
        /// идентиф
        /// </summary>
        
        public  Guid? Id { get; set; }
      
        /// <summary>
        /// название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// значчение 
        /// </summary>
      public string Value { get; set; }
    }
}
