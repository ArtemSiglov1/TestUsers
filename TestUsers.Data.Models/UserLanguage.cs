using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;
namespace TestUsers.Data.Models
{
    public class UserLanguage
    {
     public int Id { get; set; }
public User User { get; set; }
public DateTime DateLearn { get; set; }
public int UserId { get; set; }
public int LanguageId { get; set; }
public Language Language { get; set; }
    }
}
