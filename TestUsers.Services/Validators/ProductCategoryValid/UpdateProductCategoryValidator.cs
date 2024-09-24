using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.ProductCategory;

namespace TestUsers.Services.Validators.ProductCategoryValid
{
    public class UpdateProductCategoryValidator:AbstractValidator<ProductCategoryUpdateRequest>
    {
        public UpdateProductCategoryValidator() {
            RuleFor(x => x.Id).NotNull().Must(x => x >= 0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        } 
    }
}
