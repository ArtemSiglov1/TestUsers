using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services.Validators.UserContactValid
{
    public class ContactItemValid:AbstractValidator<UserContactItem>
    {
        public ContactItemValid()
        {
            
                RuleFor(x => x.Name).NotEmpty().NotNull();
                RuleFor(x => x.Value).NotEmpty();
                
            
        }
    }
}
