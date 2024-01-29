namespace Application.Service
{
    using Application.Dto;
    using Application.Service.Event;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public class EventService : IEventService
    {
        private const string defaultFilePath = "\\tmp\\visits.log";
        private IConfiguration Configuration { get; }

        public EventService(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public Task StorageEventAsync(AddEventRequest addEventRequest)
        {
            if (addEventRequest is null || string.IsNullOrEmpty(addEventRequest.EventContent))
            {
                throw new ArgumentNullException(nameof(addEventRequest), "Invalid request, unavaliable to storage.");
            }

            var configFilePath = this.Configuration.GetSection("StorageFilePath")?.Value;
            var path = string.Concat(System.IO.Directory.GetCurrentDirectory(), string.IsNullOrEmpty(configFilePath) ? defaultFilePath : configFilePath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (!File.Exists(path))
            {
                var createdFile = File.Create(path);
                createdFile.Close();
            }

            WriteCotent(path, addEventRequest.EventContent);

            return Task.CompletedTask;
        }

        private static void WriteCotent(string path, string message)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(message);
            }
        }
    }
}