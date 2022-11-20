using BarryFamily.Synology.PhotoOrganizer.Models;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using Microsoft.Extensions.Options;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface IPhotoService
    {
        Task<IEnumerable<SynoFile>> GetUnorganizedPhotosAsync();
        Task<bool> OrganizePhotoAsync(SynoFile file);
    }

    internal class PhotoService : IPhotoService
    {
        private readonly OrganizeInfo _organizeInfo;
        private readonly ISynologyFileService _synologyFileService;
        private readonly ITokenizedFilePathService _tokoenizedFilePathService;

        public PhotoService(
            IOptions<OrganizeInfo> organizeInfoOptions,
            ISynologyFileService synologyFileService,
            ITokenizedFilePathService tokenizedFilePathService) 
        {
            _organizeInfo = organizeInfoOptions.Value;
            _synologyFileService = synologyFileService;
            _tokoenizedFilePathService = tokenizedFilePathService;
        }

        public async Task<IEnumerable<SynoFile>> GetUnorganizedPhotosAsync()
        {
            if (string.IsNullOrWhiteSpace(_organizeInfo.MobileUploadPath))
            {
                throw new Exception(
                    $"Expected a value in appsettings for {nameof(OrganizeInfo)}.{nameof(OrganizeInfo.MobileUploadPath)}");
            }
            return await _synologyFileService.GetFilesAsync(_organizeInfo.MobileUploadPath!);
        }

        public async Task<bool> OrganizePhotoAsync(SynoFile file)
        {
            await _synologyFileService.CreateFolderAsync(file.Path);
            var filePath = $"{file.Path}/{file.Name}";
            var destinationPath = _tokoenizedFilePathService
                .GetTokenizedFilePath(_organizeInfo.DestinationPath, file);
            return await _synologyFileService.MoveFileAsync(filePath, destinationPath);
        }
    }
}
