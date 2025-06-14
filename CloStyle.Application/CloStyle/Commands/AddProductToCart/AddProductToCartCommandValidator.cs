using CloStyle.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddProductToCart
{
    public class AddProductToCartCommandValidator : AbstractValidator<AddProductToCartCommand>
    {
        public AddProductToCartCommandValidator()
        {
            RuleFor(s => s.SizeId)
                .NotNull().WithMessage("Please select size you want to add");
            RuleFor(a => a.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
