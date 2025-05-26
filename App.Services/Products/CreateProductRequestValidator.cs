using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator() 
        {
            RuleFor(x => x.Name)
                //.NotNull().WithMessage("Name is required!")
                .NotEmpty().WithMessage("Name is required!")
                .Length(3,10).WithMessage("Name length must be between 3 and 10!");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0!");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1,100).WithMessage("Stock must be between 1 and 100!");
        }
    }
}
