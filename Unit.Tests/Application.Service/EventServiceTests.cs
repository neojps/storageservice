using Application.Dto;
using Application.Service;
using Application.Service.Event;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Unit.Tests.Application.Service
{
    public class EventServiceTests
    {
        private readonly IEventService eventService;
        private readonly Mock<IConfiguration> mockConfiguration;

        public EventServiceTests()
        {
            this.mockConfiguration = new Mock<IConfiguration>();
            this.mockConfiguration.Setup(ic => ic.GetSection(It.IsAny<string>()).Value).Returns("\\tmp\\test-visits.log");

            this.eventService = new EventService(this.mockConfiguration.Object);
        }

        [Fact]
        public async Task StorageEventAsync_Ok()
        {
            var request = new AddEventRequest()
            {
                EventContent = "event-to-storage",
            };

            var result = Record.ExceptionAsync(async () => await eventService.StorageEventAsync(request));

            Assert.NotNull(result);
            Assert.Null(result.Result);
        }

        [Fact]
        public async Task StorageEventAsync_Fail()
        {
            AddEventRequest request = null;

            var result = Record.ExceptionAsync(async () => await eventService.StorageEventAsync(request));

            Assert.NotNull(result);
            Assert.NotNull(result.Result as ArgumentNullException);
        }
    }
}
