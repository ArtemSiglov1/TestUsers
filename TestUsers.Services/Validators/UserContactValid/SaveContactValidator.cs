using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services.Validators.UserContactValid
{
    public class SaveContactValidator:AbstractValidator<UserContactSaveRequest>
    {
        public SaveContactValidator()
        {
            RuleFor(x => x.Contacts).NotEmpty();
            RuleFor(x => x.Contacts).ForEach(x => x.SetValidator(new ContactItemValid()));
        }
    }
}
