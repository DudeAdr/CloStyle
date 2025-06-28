using Xunit;
using CloStyle.Application.CurrentApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CloStyle.Domain.Interfaces;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using CloStyle.Domain.Entities;
using FluentAssertions;

namespace CloStyle.Application.CurrentApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturCurrentUser()
        {
            //arrange

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Role, "User")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var brands = new List<Brand>
            {
                new Brand { Name = "Nike" },
                new Brand { Name = "Adidas" }
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(repo => repo.GetUserBrandsAsync("1"))
                .ReturnsAsync(brands);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext() 
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessorMock.Object, userRepositoryMock.Object);

            //act

            var currentUser = userContext.GetCurrentUser();

            //arrange

            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().Contain("User");
            currentUser.Brands.Should().HaveCount(2);
        }
    }
}