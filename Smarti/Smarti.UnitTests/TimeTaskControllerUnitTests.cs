using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Smarti.Controllers;
using Smarti.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Smarti.UnitTests
{
    class TimeTaskControllerUnitTests
    {
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMqttAppClientSingleton _mqttAppClientSingleton;
        private readonly IMapper _mapper;

        public TimeTaskControllerUnitTests()
        {
            _timeTaskRepository = Substitute.For<ITimeTaskRepository>();
            _socketRepository = Substitute.For<ISocketRepository>();
            _authorizationService = Substitute.For<IAuthorizationService>();
            _mqttAppClientSingleton = Substitute.For<IMqttAppClientSingleton>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Index_WhenCalledWithInt_ShouldNotReturnNull()
        {
            // Arrange
            TimeTaskController controller = new TimeTaskController(_timeTaskRepository, _socketRepository, 
                _authorizationService, _mqttAppClientSingleton, _mapper);

            // Act
            ViewResult viewResult = controller.Index(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Create_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            TimeTaskController controller = new TimeTaskController(_timeTaskRepository, _socketRepository,
                _authorizationService, _mqttAppClientSingleton, _mapper);

            // Act
            ViewResult viewResult = controller.Create(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }


        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            TimeTaskController controller = new TimeTaskController(_timeTaskRepository, _socketRepository,
                _authorizationService, _mqttAppClientSingleton, _mapper);

            // Act
            ViewResult viewResult = controller.Edit(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            TimeTaskController controller = new TimeTaskController(_timeTaskRepository, _socketRepository,
                _authorizationService, _mqttAppClientSingleton, _mapper);

            // Act
            ViewResult viewResult = controller.Delete(1).GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }
    }
}
