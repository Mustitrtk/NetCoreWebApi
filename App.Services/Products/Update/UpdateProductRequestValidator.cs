using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() 
        {
            RuleFor(x => x.Name)
               //.NotNull().WithMessage("Ürün ismi gereklidir.")
               .NotEmpty().WithMessage("ürün ismi gereklidir.")
               .Length(3, 10).WithMessage("ürün ismi  3 ile 10 karakter arasında olmalıdır.");

            // price validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required!")
                .GreaterThan(0).WithMessage("Category must be greater than 0!");

            //stock inclusiveBetween validation
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("stok adedi 1 ile 100 arasında olmalıdır");
        }
    }
}
