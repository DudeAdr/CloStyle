using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.EditBrand
{
    public class EditBrandCommandValidator : AbstractValidator<EditBrandCommand>
    {
        public EditBrandCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Please insert brand name")
                .MinimumLength(2).WithMessage("Brand name must be at least 2 characters");
        }
    }
}
