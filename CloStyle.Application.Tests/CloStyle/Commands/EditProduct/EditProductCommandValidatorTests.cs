using Xunit;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Linq;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;

public class EditProductCommandValidatorTests
{
    private readonly EditProductCommandValidator _validator = new();

    private ProductFormDto CreateValidCommand()
    {
        return new ProductFormDto
        {
            Name = "Valid Name",
            Price = 99.99m,
            Description = "Valid description",
            CategoryId = 1,
            GenderId = 1,
            Sizes = new List<SizeDto> { new SizeDto { Stock = 5 } },
            IsEditable = true
        };
    }

    [Fact]
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
    [InlineData(null)]
    [InlineData("")]
    public void Name_EmptyOrNull_ShouldHaveValidationError(string name)
    {
        // arrange

        var command = CreateValidCommand();
        command.Name = name;

        // act

        var result = _validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorMessage("Please insert product name");
    }

    [Fact]
    public void Name_TooShort_ShouldHaveValidationError()
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
    [InlineData(-1)]
    public void Price_LessOrEqualZero_ShouldHaveValidationError(decimal price)
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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Description_EmptyOrNull_ShouldHaveValidationError(string description)
    {
        // arrange

        var command = CreateValidCommand();
        command.Description = description;

        // act

        var result = _validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Description)
              .WithErrorMessage("Please insert product description");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void CategoryId_Invalid_ShouldHaveValidationError(int categoryId)
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
    public void GenderId_Invalid_ShouldHaveValidationError(int genderId)
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
    public void Sizes_WithNegativeStock_ShouldHaveValidationError()
    {
        // arrange

        var command = CreateValidCommand();
        command.Sizes = new List<SizeDto> { new SizeDto { Stock = -3 } };

        // act

        var result = _validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Sizes)
              .WithErrorMessage("Stock quantity must be zero or positive");
    }

    [Fact]
    public void Sizes_AllZeroStock_ShouldHaveValidationError()
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
    public void Sizes_Null_ShouldHaveValidationError()
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
