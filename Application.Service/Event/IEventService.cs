namespace Application.Service.Event
{
    using Application.Dto;

    public interface IEventService
    {
        Task StorageEventAsync(AddEventRequest addEventRequest);
    }
}
