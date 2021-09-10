using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using FluentAssertions;
using FluentAssertions.Execution;
using FakeItEasy;
using Xunit;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Controllers;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Tests
{
    public class UsersControllerUnitTest
    {
        private IUserService _userService = A.Fake<IUserService>();
        private ILogger<UsersController> _fakeLogger = A.Fake<ILogger<UsersController>>();

        [Fact]
        public async void ReturnOk_WhenDataExists()
        {
            // Arrange
            var controller = new UsersController(_fakeLogger, _userService);
            var fakeUser1 = A.Fake<UserView>();
            var fakeUser2 = A.Fake<UserView>();
            A.CallTo(() => _userService.GetAllUsers()).Returns(new List<UserView>{ fakeUser1, fakeUser2 });

            // Act
            var result = await controller.GetUsers() as ObjectResult;

            // Assert
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(StatusCodes.Status200OK);
                (result.Value as IEnumerable<UserView>).Should().NotBeEmpty().And.HaveCount(2);
            }
        }

        [Fact]
        public async void ReturnOk_WhenDataExistsForOneUser()
        {
            // Arrange
            var controller = new UsersController(_fakeLogger, _userService);
            var fakeUser1 = A.Fake<UserView>();
            var fakeUser2 = A.Fake<UserView>();
            A.CallTo(() => _userService.GetAllUsers()).Returns(new List<UserView>{ fakeUser1, fakeUser2 });

            // Act
            var result = await controller.GetUser(fakeUser1.UniqueId) as ObjectResult;

            // Assert
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(StatusCodes.Status200OK);
                result.Value.Should().Equals(fakeUser1);
            }
        }

        [Fact]
        public void ReturnContentCreated_WhenOneUserAdded()
        {
            // Arrange
            var controller = new UsersController(_fakeLogger, _userService);
            var fakeUser = A.Fake<User>();

            // Act
            var result = controller.CreateUser(fakeUser) as ObjectResult;

            // Assert
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(StatusCodes.Status201Created);
            }
        }

        [Fact]
        public void ReturnNoContent_WhenOneUserUpdated()
        {
            // Arrange
            var controller = new UsersController(_fakeLogger, _userService);
            var fakeUser = A.Fake<User>();
            // var fakeGuid = new Guid();
            //A.CallTo(() => _userService.UpdateUser(fakeGuid, fakeUser)).Returns(true);

            // Act
            // var result = controller.UpdateUser(fakeUser.Id, fakeUser) as NoContentResult;

            // // Assert
            // using (new AssertionScope())
            // {
            //     result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            //     A.CallTo(() => _userService.UpdateUser(fakeUser)).MustHaveHappenedOnceExactly();
            // }
        }

        [Fact]
        public void ReturnBadRequest_WhenNoUserUpdated()
        {
            // Arrange
            var controller = new UsersController(_fakeLogger, _userService);
            var fakeUser = A.Fake<User>();
            fakeUser.Id = 23;
            //A.CallTo(() => _userService.UpdateUser(fakeUser)).Returns(false);

            // Act
            // var result = controller.UpdateUser(17, fakeUser) as BadRequestObjectResult;

            // // Assert
            // using (new AssertionScope())
            // {
            //     result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            //     A.CallTo(() => _userService.UpdateUser(fakeUser)).MustHaveHappenedOnceExactly();
            // }
        }

        // [Fact]
        // public void ReturnNoContent_WhenUserDeleted()
        // {
        //     // Arrange
        //     var controller = new UsersController(_fakeLogger, _userService);
        //     var fakeUser = A.Fake<User>();
        //     fakeUser.Id = 23;
        //     A.CallTo(() => _userService.DeleteUser(1)).Returns(true);

        //     // Act
        //     var result = controller.DeleteUser(1) as NoContentResult;

        //     // Assert
        //     using (new AssertionScope())
        //     {
        //         result.Should().NotBeNull();
        //         result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        //         A.CallTo(() => _userService.DeleteUser(1)).MustHaveHappenedOnceExactly();
        //     }
        // }

        // [Fact]
        // public void ReturnNotFound_WhenUserDeletedDoesNotExist()
        // {
        //     // Arrange
        //     var controller = new UsersController(_fakeLogger, _userService);
        //     var fakeUser = A.Fake<User>();
        //     fakeUser.Id = 23;
        //     A.CallTo(() => _userService.DeleteUser(1)).Returns(false);

        //     // Act
        //     var result = controller.DeleteUser(1) as NotFoundObjectResult;

        //     // Assert
        //     using (new AssertionScope())
        //     {
        //         result.Should().NotBeNull();
        //         result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        //         A.CallTo(() => _userService.DeleteUser(1)).MustHaveHappenedOnceExactly();
        //     }
        // }
    }
}
