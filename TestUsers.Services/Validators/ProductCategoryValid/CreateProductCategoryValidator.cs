using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.ProductCategory;

namespace TestUsers.Services.Validators.ProductCategoryValid
{
    public class CreateProductCategoryValidator:AbstractValidator<ProductCategoryCreateRequest>
    {
        public CreateProductCategoryValidator() { 
        RuleFor(x=>x.Name).NotEmpty().NotNull();
        }
    }
}
