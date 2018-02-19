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
    class StatisticsControllerUnitTests
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IChartGenerator _chartGenerator;

        public StatisticsControllerUnitTests()
        {
            _roomRepository = Substitute.For<IRoomRepository>();
            _chartGenerator = Substitute.For<IChartGenerator>();
        }

        [Fact]
        public void Index_WhenCalledWithDateTimeAndDateTime_ShouldNotReturnNull()
        {
            // Arrange
            StatisticsController controller = new StatisticsController(_roomRepository, _chartGenerator);

            // Act
            ViewResult viewResult = controller.Index(new DateTime(1, 1, 1), new DateTime(1, 1, 2)) as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }
    }
}
