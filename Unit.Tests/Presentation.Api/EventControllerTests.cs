using Application.Dto;
using Application.Service.Event;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Api.Controllers;
using Xunit;

namespace Unit.Tests.Presentation.Api
{
    public class EventControllerTests
    {
        private readonly EventController eventController;
        private readonly Mock<IEventService> eventServiceMock;

        public EventControllerTests()
        {
            this.eventServiceMock = new Mock<IEventService>();
            this.eventServiceMock.Setup(ie => ie.StorageEventAsync(It.IsAny<AddEventRequest>())).Returns(Task.CompletedTask);

            this.eventController = new EventController(this.eventServiceMock.Object);
        }

        [Fact]
        public async Task PostEventAsync_Ok()
        {
            var request = new AddEventRequest()
            {
                EventContent = "event-to-storage",
            };

            var response = await this.eventController.PostEventAsync(request);

            Assert.NotNull(response as OkResult);
        }

        [Fact]
        public async Task PostEventAsync_NotOk()
        {
            AddEventRequest request = null;

            var response = await eventController.PostEventAsync(request);

            Assert.NotNull(response as BadRequestObjectResult);
        }
    }
}
