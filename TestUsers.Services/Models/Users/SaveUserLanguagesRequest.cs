using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    public class SaveUserLanguagesRequest
    {
        public int UserId{ get; set; }
    public List<SaveUserLanguageItem> Items { get; set; }

    }
}
