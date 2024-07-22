using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    internal class AddLanguageToUser
    {
       public Guid UserId { get; set; }
public int LanguageId { get; set; }
public DateTime DateLearn { get; set; }
    }
}
