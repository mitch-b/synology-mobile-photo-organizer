using BarryFamily.Synology.PhotoOrganizer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarryFamily.Synology.PhotoOrganizer
{
    internal class PhotoOrganizerWorker : IHostedService
    {
        private readonly ILogger<PhotoOrganizerWorker> _logger;
        private readonly IPhotoService _photoService;
        private readonly int _checkEverySeconds = 60;

        public PhotoOrganizerWorker(ILoggerFactory loggerFactory, IPhotoService photoService)
        {
            _logger = loggerFactory.CreateLogger<PhotoOrganizerWorker>();
            _photoService = photoService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting Photo Organizer!");
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _logger.LogInformation($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}: Checking for photos to organize");
                var unorganizedPhotos = await _photoService.GetUnorganizedPhotosAsync();
                foreach (var photo in unorganizedPhotos)
                {
                    _logger.LogInformation($"Organizing -- {photo.Name}");
                    await _photoService.OrganizePhotoAsync(photo);
                    _logger.LogDebug($"{photo.Name} -- Finished!");
                }
                await Task.Delay(_checkEverySeconds * 1000);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
