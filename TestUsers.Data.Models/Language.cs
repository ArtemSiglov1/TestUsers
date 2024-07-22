using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestUsers.Data.Models 
{
    public class Language
    {
      public  int Id { get; set; }
public string Code { get; set; }//ru, en
public string Name { get; set; }//Russian, English
public List<UserLanguage> Users { get; set; }
    }
}
