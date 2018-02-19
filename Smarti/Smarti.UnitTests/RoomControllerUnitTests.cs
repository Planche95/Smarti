using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Smarti.Controllers;
using Smarti.Models;
using Smarti.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Smarti.UnitTests
{
    class RoomControllerUnitTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public RoomControllerUnitTests()
        {
            _userManager = Substitute.For<UserManager<ApplicationUser>>();
            _roomRepository = Substitute.For<IRoomRepository>();
            _timeTaskRepository = Substitute.For<ITimeTaskRepository>();
            _authorizationService = Substitute.For<IAuthorizationService>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Create_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            RoomController controller = new RoomController(_userManager, _roomRepository, _timeTaskRepository, 
                                                        _authorizationService, _mapper);

            // Act
            ViewResult viewResult = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }


        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            RoomController controller = new RoomController(_userManager, _roomRepository, _timeTaskRepository,
                                                        _authorizationService, _mapper);

            // Act
            ViewResult viewResult = controller.Edit(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            RoomController controller = new RoomController(_userManager, _roomRepository, _timeTaskRepository,
                                                        _authorizationService, _mapper);

            // Act
            ViewResult viewResult = controller.Delete(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }
    }
}
