using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services.Validators.UserLanguageValid
{
    public class SaveUserLanguageValidator:AbstractValidator<SaveUserLanguagesRequest>
    {
        public SaveUserLanguageValidator() { 
            RuleFor(x=>x.Items).NotEmpty().ForEach(x=>x.SetValidator(new SaveUserLanguageItemValidator()));
        }
    }
}
