using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using StockManagment.Api.Controllers.v1;
using StockManagment.DataServices.IConfiguration;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Generic;
using StockManagment.Entities.DTOs.Incoming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace StockManagment.Api.Test
{
    public class UserControllerTest
    {
        private  Mock<IMapper> mapperStub = new Mock<IMapper>();
        private  Mock<UserManager<IdentityUser>> userManagerStub = new Mock<UserManager<IdentityUser>>();
        private  Mock<IUnitOfWork> UnitOfWorkStub = new Mock<IUnitOfWork>();



        [Fact]
        public void PostUser_WithUserToCreate_ReturnsCreatedUser()
        {
            // Arrange
            var userToCreate = new UserDTO()
            {
                FirstName = "Joseph",
                LastName = "Tribuani",
                Email = "yura@gmail.com",
                Phone = "9989700000",
                DateOfBirth = "09/27/1997"
            };

            var createdUser = new User()
            {
                Id = new Guid("1a14874a-9c42-4328-a8b6-2f966b815067"),
                FirstName = "Joseph",
                LastName = "Tribuani",
                Email = "yura@gmail.com",
                Phone = "9989700000",
                DateOfBirth = Convert.ToDateTime("09/27/1997"),
                Status = 1,
                AddedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            var controller = new UserController(UnitOfWorkStub.Object, null, mapperStub.Object);
            UnitOfWorkStub.Setup(x => x.UserRepository.Add(It.IsAny<User>()))
                .ReturnsAsync(true);
            mapperStub.Setup( x => x.Map<User>(It.IsAny<UserDTO>()))
                .Returns(createdUser);
            // Act
            var actualResult = controller.PostUser(userToCreate);

            // Assert
            var expectedResult = (actualResult.Result as CreatedAtRouteResult).Value as Result<User>;

            Assert.IsType<CreatedAtRouteResult>(actualResult.Result);
            createdUser.Should().BeEquivalentTo(expectedResult.Content);
            
        }


        [Fact]
        public void PostUser_WithUserToCreate_ReturnsValidationError()
        {
            // Arrange
            var userToCreate = new UserDTO()
            {
                FirstName = "Joseph",
                LastName = "Tribuani",
                Email = "yura@gmail.com",
                Phone = "",
                DateOfBirth = "09/27/1997"
            };
            
            var controller = new UserController(UnitOfWorkStub.Object, null, mapperStub.Object);
            controller.ModelState.Clear();
            var context = new ValidationContext(userToCreate, null, null);
            var results = new List<ValidationResult>();

            // Act
            var actualResult = controller.PostUser(userToCreate);
            TypeDescriptor.AddProviderTransparent(
            new AssociatedMetadataTypeTypeDescriptionProvider(typeof(UserDTO), typeof(UserDTO)), typeof(UserDTO));
            var isValid = Validator.TryValidateObject(userToCreate, context, results, true);
            
            // Assert
            Assert.False(isValid);

        }
    }
}