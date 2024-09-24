using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services.Validators.UserLanguageValid
{
    public class SaveUserLanguageItemValidator:AbstractValidator<SaveUserLanguageItem>
    {
        public SaveUserLanguageItemValidator() {
            RuleFor(x=>x.DateLearn).NotNull()
                .Must(x => x <= DateTime.UtcNow && x.Year > DateTime.Now.Year - 100);
                RuleFor(x => x.LanguageId).NotNull().NotEmpty();
            
        }
    }
}
