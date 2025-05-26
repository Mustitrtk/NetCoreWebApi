using App.Repositories.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator() 
        {
            RuleFor(x => x.Name)
                //.NotNull().WithMessage("Name is required!")
                .NotEmpty().WithMessage("Name is required!")
                .Length(3,10).WithMessage("Name length must be between 3 and 10!")
                .MustAsync(MustUniqueProductNameAsync).WithMessage("The name is already exist!");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0!");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1,100).WithMessage("Stock must be between 1 and 100!");
        }

        #region 2.way async validation

        private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
        {
            return !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
        }

        #endregion


        #region 1.way sync validation

        //private bool MustUniqueProductName(string name)
        //{
        //    return !_productRepository.Where(x => x.Name == name).Any();

        //    // false => bir hata var.
        //    // true => bir hata yok
        //}

        #endregion
    }
}
