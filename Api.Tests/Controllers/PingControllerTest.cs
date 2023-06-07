using DogApp.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.Controllers
{
    public class PingControllerTest
    {
        private readonly PingController _pingController;

        public PingControllerTest()
        {
            _pingController = new PingController();
        }

        [Fact]
        public void Ping_IfCorrectRequestProvided_RetrnOkObjectResult()
        {
            var expectedResult = new OkObjectResult("Dogs house service. Version 1.0.1");

            var actualResult =  _pingController.Ping(CancellationToken.None);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}