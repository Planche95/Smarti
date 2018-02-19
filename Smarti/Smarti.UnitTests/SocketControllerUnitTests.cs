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
    class SocketControllerUnitTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMqttAppClient _mqttAppClient;
        private readonly IMapper _mapper;

        public SocketControllerUnitTests()
        {
            _userManager = Substitute.For<UserManager<ApplicationUser>>();
            _roomRepository = Substitute.For<IRoomRepository>();
            _socketRepository = Substitute.For<ISocketRepository>();
            _timeTaskRepository = Substitute.For<ITimeTaskRepository>();
            _authorizationService = Substitute.For<IAuthorizationService>();
            _mqttAppClient = Substitute.For<IMqttAppClient>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Index_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            SocketController controller = new SocketController(_userManager, _roomRepository, _socketRepository,
                _timeTaskRepository, _authorizationService, _mqttAppClient, _mapper);

            // Act
            ViewResult viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Create_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            SocketController controller = new SocketController(_userManager, _roomRepository, _socketRepository,
                _timeTaskRepository, _authorizationService, _mqttAppClient, _mapper);

            // Act
            ViewResult viewResult = controller.Create(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }


        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            SocketController controller = new SocketController(_userManager, _roomRepository, _socketRepository,
                _timeTaskRepository, _authorizationService, _mqttAppClient, _mapper);

            // Act
            ViewResult viewResult = controller.Edit(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            SocketController controller = new SocketController(_userManager, _roomRepository, _socketRepository,
                _timeTaskRepository, _authorizationService, _mqttAppClient, _mapper);

            // Act
            ViewResult viewResult = controller.Delete(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }
    }
}
