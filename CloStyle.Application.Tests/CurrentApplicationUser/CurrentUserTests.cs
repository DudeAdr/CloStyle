using Xunit;
using CloStyle.Application.CurrentApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloStyle.Domain.Entities;
using FluentAssertions;

namespace CloStyle.Application.CurrentApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            //arrange
            
            var currentUser = new CurrentUser("1","testmail@test.com", new List<string>{"Admin"}, new List<Brand> { });

            //act
            var isInRole = currentUser.IsInRole("Admin");

            //assert
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithoutMatchingRole_ShouldReturnFalse()
        {
            //arrange

            var currentUser = new CurrentUser("1", "testmail@test.com", new List<string> { "Admin" }, new List<Brand> { });

            //act
            var isInRole = currentUser.IsInRole("Owner");

            //assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNotMatchingLetterCase_ShouldReturnFalse()
        {
            //arrange

            var currentUser = new CurrentUser("1", "testmail@test.com", new List<string> { "Admin" }, new List<Brand> { });

            //act
            var isInRole = currentUser.IsInRole("admin");

            //assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void HasSpaceForBrandTest_InSpaceRange_ShouldReturnTrue()
        {
            //arrange

            var currentUser = new CurrentUser("1", "testmail@test.com", new List<string> { "Admin" }, new List<Brand> { new Brand()});

            //act

            var hasSpace = currentUser.HasSpaceForBrand();

            //assert

            hasSpace.Should().BeTrue();
        }

        [Fact()]
        public void HasSpaceForBrandTest_NotInSpaceRange_ShouldReturnFalse()
        {
            //arrange

            var currentUser = new CurrentUser("1", "testmail@test.com", new List<string> { "Admin" }, new List<Brand> { new Brand(), new Brand(), new Brand(), new Brand(), new Brand()});

            //act

            var hasSpace = currentUser.HasSpaceForBrand();

            //assert

            hasSpace.Should().BeFalse();
        }
    }
}