using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarryFamily.Synology.PhotoOrganizer
{
    internal class PhotoOrganizerWorker : IHostedService
    {
        private readonly ILogger<PhotoOrganizerWorker> _logger;
        public PhotoOrganizerWorker(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PhotoOrganizerWorker>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting Photo Organizer!");
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _logger.LogInformation($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} {nameof(PhotoOrganizerWorker)}: Checking for photos to organize");
                // var unorganizedPhotos = await photoService.GetUnorganizedPhotosAsync();
                // var result = await organizerService.MovePhotosAsync(unorganizedPhotos);
                await Task.Delay(5000);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
