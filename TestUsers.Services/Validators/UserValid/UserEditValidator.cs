using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services.Validators.UserValid
{
    public class UserEditValidator:AbstractValidator<UserEditRequest>
    {
        public UserEditValidator(IQueryable<User> query) { 
        RuleFor(x=>x.FullName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x).Must(x => !query.Where(u => u.Email == x.Email && u.Id == x.Id).Any());
        }
    }
}
