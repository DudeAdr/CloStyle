using CloStyle.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddBrand
{
    public class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
    {
        public AddBrandCommandValidator(IBrandRepository repository)
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Please insert brand name")
                .MinimumLength(2).WithMessage("Brand name must be at least 2 characters")
                .Custom((value, context) =>
                {
                    var existingBrand = repository.GetByName(value).Result;
                    if (existingBrand != null)
                    {
                        context.AddFailure($"Brand {value} already exists in database");
                    }
                });
        }
    }
}

