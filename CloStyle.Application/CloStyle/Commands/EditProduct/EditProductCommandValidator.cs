using CloStyle.Application.CloStyle.Dtos;
using FluentValidation;

namespace CloStyle.Application.CloStyle.Commands.EditProduct
{ 
    public class EditProductCommandValidator : AbstractValidator<EditProductDto>
    {
        public EditProductCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Please insert product name")
                .MinimumLength(2).WithMessage("Product name must be at least 2 characters");

            RuleFor(a => a.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than 0");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Please insert product description");

            RuleFor(a => a.CategoryId)
                .GreaterThan(0).WithMessage("Please select product category");

            RuleFor(a => a.GenderId)
                .GreaterThan(0).WithMessage("Please select product gender");

            RuleForEach(a => a.Sizes)
                .Must(size => size.Stock >= 0)
                .WithMessage("Stock quantity must be zero or positive");

            RuleFor(a => a.Sizes)
                .Must(sizes => sizes != null && sizes.Any(s => s.Stock > 0))
                .WithMessage("Please enter stock for at least one size");
        }
    }
}
