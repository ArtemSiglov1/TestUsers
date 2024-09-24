using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.News;

namespace TestUsers.Services.Validators.NewsValid
{
    public class EditNewsValidator : AbstractValidator<NewsEditRequest>
    {

        public EditNewsValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.AuthorId).NotNull().NotEmpty();
            RuleFor(x => x.Title).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Description).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Tags).NotEmpty();
        }
    }
}
