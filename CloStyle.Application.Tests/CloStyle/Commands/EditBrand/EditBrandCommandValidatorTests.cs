using Xunit;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace CloStyle.Application.CloStyle.Commands.EditBrand.Tests
{
    public class EditBrandCommandValidatorTests
    {
        private EditBrandCommand CreateValidCommand()
        {
            return new EditBrandCommand
            {
                Id = 1,
                Name = "Test Brand",
                ImgPath = "test.jpg",
                IsEditable = true
            };
        }
        private readonly EditBrandCommandValidator _validator = new();

        [Fact()]
        public void ValidCommand_ShouldPassValidation()
        {
            // arrange

            var command = CreateValidCommand();

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void NameTooShort_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Name = "T";

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("Brand name must be at least 2 characters");
        }

        [Fact()]
        public void NameEmpty_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Name = null;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("Please insert brand name");
        }
    }
}