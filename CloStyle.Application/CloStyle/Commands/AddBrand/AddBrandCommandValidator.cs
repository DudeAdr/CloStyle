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
                    var existingBrand = repository.GetBrandByName(value).Result;
                    if (existingBrand != null)
                    {
                        context.AddFailure($"Brand {value} already exists in database");
                    }
                });
            RuleFor(a => a.ImageFile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Please upload a logo image.")
                .Must(file => file != null && file.Length > 0)
                    .WithMessage("Uploaded file is empty.")
                .Must(file =>
                {
                    if (file == null) return false;

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                    return extension != null && allowedExtensions.Contains(extension);
                }).WithMessage("Only image files (jpg, jpeg, png, gif) are allowed.");

        }
    }
}

