using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    internal class UserContactSaveRequest
    {
        public UserContactSaveRequest()
        {
        }

        public UserContactSaveRequest(Guid userId, List<UserContactItem> contacts)
        {
            UserId = userId;
            Contacts = contacts;
        }

        public Guid UserId { get; set; }
public List<UserContactItem>? Contacts { get;set; }
    }
}
