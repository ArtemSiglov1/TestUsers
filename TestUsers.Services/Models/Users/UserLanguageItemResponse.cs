using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    public class UserLanguageItemResponse
    {
       public int LanguageId { get; set; }
public string Code { get; set; }  //ru, en
public string Name { get; set; }//Russian, English
public DateTime DateLearn { get; set; }
    }
}
