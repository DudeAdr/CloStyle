using Xunit;
using CloStyle.Application.CloStyle.Commands.AddProductToCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace CloStyle.Application.CloStyle.Commands.AddProductToCart.Tests
{
    public class AddProductToCartCommandValidatorTests
    {
        private AddProductToCartCommand CreateValidCommand()
        {
            return new AddProductToCartCommand
            {
                Id = 1,
                BrandId = 1,
                BrandName = "Test Brand",
                SizeId = 1,
                Quantity = 5
            };
        }
        private readonly AddProductToCartCommandValidator _validator = new();

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

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void QuantityLessThanOrEqualZero_ShouldHaveValidationError(int quantity)
        {
            // arrange

            var command = CreateValidCommand();
            command.Quantity = quantity;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Quantity)
                  .WithErrorMessage("Quantity must be greater than 0.");
        }
    }
}