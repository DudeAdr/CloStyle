using Xunit;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using Moq;
using FluentValidation.TestHelper;

namespace CloStyle.Application.CloStyle.Commands.AddBrand.Tests
{
    public class AddBrandCommandValidatorTests
    {
        private IFormFile GetValidImage()
        {
            return new FormFile(new MemoryStream(new byte[] { 1, 2, 3 }), 0, 3, "file", "logo.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
        }

        [Fact()]
        public void AddBrandCommandValidatorTest_NameAlreadyExistsInDb_ShouldReturnError()
        {
            //arrange

            var brand = new Brand()
            {
                Id = 1,
                Name = "Test Brand",
                ImgPath = "test.jpg"
            };

            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock
                .Setup(repo => repo.GetBrandByName("Test Brand"))
                .Returns(Task.FromResult(brand));

            IFormFile formFile = new FormFile(new MemoryStream(), 0, 0, "file", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var AddBrandCommand = new AddBrandCommand
            {
                Id = 2,
                Name = "Test Brand",
                ImageFile = formFile,
                ImgPath = "test.jpg",
                IsEditable = true
            };

            var validator = new AddBrandCommandValidator(brandRepositoryMock.Object);

            //act

            var result = validator.TestValidate(AddBrandCommand);

            //assert

            result.ShouldHaveValidationErrorFor(a => a.Name);
        }


        [Fact]
        public void AddBrandCommandValidatorTest_ValidData_ShouldNotReturnErrors()
        {
            // arrange
            var brandRepositoryMock = new Mock<IBrandRepository>();
            brandRepositoryMock
                .Setup(repo => repo.GetBrandByName("Unique Brand"))
                .Returns(Task.FromResult<Brand>(null));

            IFormFile formFile = new FormFile(new MemoryStream(new byte[] { 1, 2, 3 }), 0, 3, "file", "logo.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            var command = new AddBrandCommand
            {
                Id = 3,
                Name = "Unique Brand",
                ImageFile = formFile,
                ImgPath = "logo.png",
                IsEditable = true
            };

            var validator = new AddBrandCommandValidator(brandRepositoryMock.Object);

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddBrandCommandValidatorTest_EmptyName_ShouldReturnError()
        {
            //arrange
            var repoMock = new Mock<IBrandRepository>();
            var validator = new AddBrandCommandValidator(repoMock.Object);

            var command = new AddBrandCommand
            {
                Name = "",
                ImageFile = GetValidImage()
            };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("Please insert brand name");
        }


        [Fact]
        public void AddBrandCommandValidatorTest_ShortName_ShouldReturnError()
        {
            // arrange
            var repoMock = new Mock<IBrandRepository>();
            var validator = new AddBrandCommandValidator(repoMock.Object);

            var command = new AddBrandCommand
            {
                Name = "A",
                ImageFile = GetValidImage()
            };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("Brand name must be at least 2 characters");
        }


        [Fact]
        public void AddBrandCommandValidatorTest_NullImageFile_ShouldReturnError()
        {
            // arrange
            var repoMock = new Mock<IBrandRepository>();
            var validator = new AddBrandCommandValidator(repoMock.Object);

            var command = new AddBrandCommand
            {
                Name = "Valid Brand",
                ImageFile = null
            };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.ImageFile)
                .WithErrorMessage("Please upload a logo image.");
        }

        [Fact]
        public void AddBrandCommandValidatorTest_EmptyImageFile_ShouldReturnError()
        {
            // arrange
            var repoMock = new Mock<IBrandRepository>();
            var validator = new AddBrandCommandValidator(repoMock.Object);

            IFormFile emptyFile = new FormFile(new MemoryStream(), 0, 0, "file", "logo.jpg");

            var command = new AddBrandCommand
            {
                Name = "Valid Brand",
                ImageFile = emptyFile
            };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.ImageFile)
                .WithErrorMessage("Uploaded file is empty.");
        }

        [Fact]
        public void AddBrandCommandValidatorTest_InvalidFileExtension_ShouldReturnError()
        {
            // arrange
            var repoMock = new Mock<IBrandRepository>();
            var validator = new AddBrandCommandValidator(repoMock.Object);

            IFormFile file = new FormFile(new MemoryStream(new byte[] { 1, 2, 3 }), 0, 3, "file", "logo.exe");

            var command = new AddBrandCommand
            {
                Name = "Valid Brand",
                ImageFile = file
            };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.ImageFile)
                .WithErrorMessage("Only image files (jpg, jpeg, png, gif) are allowed.");
        }



    }
}