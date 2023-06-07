using System.Net;
using DogApp.Api.Middlewares;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Api.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTest
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ErrorHandlingMiddlewareTest()
        {
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
        }

        [Fact]
        public async Task Handle_IfInvalidOperationExceptionThrown_ReturnBadRequest()
        {
            var middleware = new ErrorHandlingMiddleware(context => throw new InvalidOperationException("Test exception"));
            var context = new DefaultHttpContext();

            await middleware.InvokeAsync(context, _webHostEnvironment);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Handle_IfKeyNotFoundExcepitonThrown_ReturnBadRequest()
        {
            var middleware = new ErrorHandlingMiddleware(context => throw new KeyNotFoundException("Test exception"));
            var context = new DefaultHttpContext();

            await middleware.InvokeAsync(context, _webHostEnvironment);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Handle_IfAnyExceptionThrown_ReturnInternaServerError()
        {
            var middleware = new ErrorHandlingMiddleware(context => throw new AggregateException("Test exception"));
            var context = new DefaultHttpContext();

            await middleware.InvokeAsync(context, _webHostEnvironment);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}