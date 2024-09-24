using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.ProductCategoryParameter;

namespace TestUsers.Services.Validators.CategoryParametersValid
{
    public class CreateCategoryParametersValidator:AbstractValidator<ProductCategoryParameterCreateRequest>
    {
        public CreateCategoryParametersValidator() { 
        RuleFor(x=>x.ProductCategoryId).NotEmpty().NotNull();
            RuleFor(x=>x.Name).NotEmpty().NotNull();
            RuleFor(x=>x.Values).NotEmpty();
        
        }
    }
}
