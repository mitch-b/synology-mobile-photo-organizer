using BarryFamily.Synology.PhotoOrganizer.Models;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Synology.Api.Client;
using Synology.Api.Client.Apis.FileStation.CopyMove.Models;
using Synology.Api.Client.Apis.FileStation.List.Models;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface ISynologyFileService
    {
        Task<IEnumerable<SynoFile>> GetFilesAsync(string path);
        Task<bool> MoveFileAsync(string sourcePath, string destinationPath);
        Task<bool> CreateFolderAsync(string path);
    }

    internal class SynologyFileService : ISynologyFileService
    {
        private readonly ILogger<SynologyFileService> _logger;
        private readonly ISynologyClient _synologyClient;
        private readonly SynologyConnection _synologyConnection;

        private readonly int _fileMoveStatusCheckWaitPeriodMilliseconds = 500;

        public SynologyFileService(
            ILoggerFactory loggerFactory,
            IOptions<SynologyConnection> synologyConnectionOptions)
        {
            _logger = loggerFactory.CreateLogger<SynologyFileService>();
            _synologyConnection = synologyConnectionOptions.Value;
            _synologyClient = new SynologyClient(_synologyConnection.Host);
        }

        public async Task<bool> CreateFolderAsync(string path)
        {
            if (!_synologyClient.IsLoggedIn) await LoginAsync();
            var response = await _synologyClient.FileStationApi().CreateFolderEndpoint().CreateAsync(new[] { path }, true);
            return response.Folders?.FirstOrDefault()?.Path == path;
        }

        public async Task<IEnumerable<SynoFile>> GetFilesAsync(string path)
        {
            if (!_synologyClient.IsLoggedIn) await LoginAsync();
            var response = await _synologyClient.FileStationApi().ListEndpoint().ListAsync(new FileStationListRequest(path));
            return response.Files.Select(f => new SynoFile()
            {
                Path = f.Path,
                Name = f.Name,
                Date = new DateTime((long)f.Additional.Time.Mtime)
            });
        }

        public async Task<bool> MoveFileAsync(string sourcePath, string destinationPath)
        {
            if (!_synologyClient.IsLoggedIn) await LoginAsync();

            _logger.LogDebug($"Requesting file move from {sourcePath} to {destinationPath}.");
            var response = await _synologyClient.FileStationApi().CopyMoveEndpoint()
                .StartMoveAsync(new[] { sourcePath }, destinationPath, true);
            _logger.LogDebug($"Move requested.");

            FileStationCopyMoveStatusResponse? statusResponse = null;
            var taskDone = false;
            var checkCounter = 1;

            while (!taskDone)
            {
                _logger.LogDebug($"Checking status of file move");
                statusResponse = await _synologyClient.FileStationApi()
                    .CopyMoveEndpoint()
                    .GetStatusAsync(response.TaskId);
                _logger.LogDebug($"Got status of file move - Finished? {statusResponse.Finished}");

                taskDone = statusResponse.Finished;
                if (!taskDone)
                {
                    _logger.LogInformation($"Long-running operation for task {response.TaskId}. Checked {checkCounter++} times.");
                    await Task.Delay(_fileMoveStatusCheckWaitPeriodMilliseconds);
                }
            }
            return statusResponse?.Finished == true;
        }

        private async Task LoginAsync()
        {
            _logger.LogDebug("Logging in...");
            await _synologyClient.LoginAsync(_synologyConnection.Username, _synologyConnection.Password);
            _logger.LogDebug("Login finished");
        }
    }
}
