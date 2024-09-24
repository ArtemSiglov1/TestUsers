using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models.Product;

namespace TestUsers.Services.Validators.ProductValid
{
    public class ProductSaveRequestValidator:AbstractValidator<ProductSaveRequest>
    {
        public ProductSaveRequestValidator() { 
        RuleFor(x=>x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Amount).NotEmpty().NotNull().Must(x =>x>0);
            RuleFor(x => x.Description).MaximumLength(100).NotNull().NotEmpty();
            RuleFor(x=>x.CategoryName).NotEmpty().NotNull();
        }
    }
}
