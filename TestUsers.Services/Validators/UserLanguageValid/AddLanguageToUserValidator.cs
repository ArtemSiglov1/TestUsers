using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;
using TestUsers.Services.Models.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestUsers.Services.Validators.UserLanguageValid
{
    public class AddLanguageToUserValidator:AbstractValidator<AddLanguageToUser>
    {
        public AddLanguageToUserValidator(IQueryable<UserLanguage> queryable) {
            RuleFor(x => x.LanguageId).NotNull().NotEmpty().MustAsync(async (x,token) =>!await queryable.AnyAsync(u => u.LanguageId == x,token));
        }
    }
}
