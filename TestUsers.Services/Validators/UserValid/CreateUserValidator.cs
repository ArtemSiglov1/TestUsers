using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Services.Models.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestUsers.Services.Validators.UserValid
{
    public class CreateUserValidator:AbstractValidator<UserCreateRequest>
    {
        public CreateUserValidator(IQueryable<User> users) {
        RuleFor(x=>x.FullName).NotNull().NotEmpty().WithMessage("FullName is null");
            RuleFor(x => x.Email).NotNull().NotEmpty().Must(x =>!users.Where(u => u.Email == x).Any());
        }
    }
}
