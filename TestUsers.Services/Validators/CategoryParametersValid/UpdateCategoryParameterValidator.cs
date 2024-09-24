using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.ProductCategoryParameter;

namespace TestUsers.Services.Validators.CategoryParametersValid
{
    internal class UpdateCategoryParameterValidator:AbstractValidator<ProductCategoryParameterUpdateRequest>
    {
        public UpdateCategoryParameterValidator() { 
        RuleFor(x=>x.Id).NotEmpty().NotNull();
            RuleFor(x=>x.Name).NotNull().NotEmpty();
            RuleFor(x=>x.ProductCategoryId).NotNull().NotEmpty();
            RuleFor(x=>x.Values).NotEmpty();
        }
    }
}
