using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    internal class UserContactItem
    {
        public UserContactItem() { }
        public UserContactItem(Guid? id, string name, string value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public  Guid? Id { get; set; }
      public string Name { get; set; }
      public string Value { get; set; }
    }
}
