using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.News;

namespace TestUsers.Services.Validators.NewsValid
{
    public class CreateNewsValidator : AbstractValidator<NewsCreateRequest>
    {
        public CreateNewsValidator()
        {
            RuleFor(x => x.Title).MaximumLength(50).NotEmpty().NotNull();
            RuleFor(x => x.AuthorId).Must(x => x > 0).NotNull();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Tags).NotEmpty();
        }
    }
}
