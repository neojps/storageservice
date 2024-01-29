using Application.Dto;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Unit.Tests.Application.Dto
{
    public class AddEventRequestTests
    {
        [Fact]
        public void AddEventRequest_Ok()
        {
            var request = new AddEventRequest()
            {
                EventContent = "event-content",
            };

            var aer = new ValidationContext(request, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(request, aer, results, true);

            Assert.True(isValid);
        }
    }
}
