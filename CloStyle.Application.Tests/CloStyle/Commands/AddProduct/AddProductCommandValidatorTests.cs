using Xunit;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using FluentValidation.TestHelper;

namespace CloStyle.Application.CloStyle.Commands.AddProduct.Tests
{
    public class AddProductCommandValidatorTests
    {
        private ProductFormDto CreateValidCommand()
        {
            return new ProductFormDto
            {
                Name = "Valid Name",
                Price = 100.0m,
                Description = "Valid description.",
                CategoryId = 1,
                GenderId = 1,
                Sizes = new List<SizeDto>
            {
                new SizeDto { Stock = 5 },
                new SizeDto { Stock = 0 }
            },
                IsEditable = true
            };
        }

        private readonly AddProductCommandValidator _validator = new();

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

        [Fact]
        public void EmptyName_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Name = "";

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name)
                  .WithErrorMessage("Please insert product name");
        }

        [Fact]
        public void NameTooShort_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Name = "A";

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name)
                  .WithErrorMessage("Product name must be at least 2 characters");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void PriceNotGreaterThanZero_ShouldHaveValidationError(decimal price)
        {
            // arrange

            var command = CreateValidCommand();
            command.Price = price;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Price)
                  .WithErrorMessage("Product price must be greater than 0");
        }

        [Fact]
        public void EmptyDescription_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Description = "";

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Description)
                  .WithErrorMessage("Please insert product description");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidCategoryId_ShouldHaveValidationError(int categoryId)
        {
            // arrange

            var command = CreateValidCommand();
            command.CategoryId = categoryId;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.CategoryId)
                  .WithErrorMessage("Please select product category");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidGenderId_ShouldHaveValidationError(int genderId)
        {
            // arrange

            var command = CreateValidCommand();
            command.GenderId = genderId;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.GenderId)
                  .WithErrorMessage("Please select product gender");
        }

        [Fact]
        public void StockBelowZero_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Sizes = new List<SizeDto> { new SizeDto { Stock = -1 } };

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Sizes)
                  .WithErrorMessage("Stock quantity must be zero or positive");
        }

        [Fact]
        public void AllStocksZero_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Sizes = new List<SizeDto>
            {
                new SizeDto { Stock = 0 },
                new SizeDto { Stock = 0 }
            };

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Sizes)
                  .WithErrorMessage("Please enter stock for at least one size");
        }

        [Fact]
        public void NullSizes_ShouldHaveValidationError()
        {
            // arrange

            var command = CreateValidCommand();
            command.Sizes = null;

            // act

            var result = _validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Sizes)
                  .WithErrorMessage("Please enter stock for at least one size");
        }
    }
}