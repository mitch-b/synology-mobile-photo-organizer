using BarryFamily.Synology.PhotoOrganizer.Models;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface IPhotoService
    {
        Task<IEnumerable<SynoFile>> GetUnorganizedPhotosAsync();
        Task<bool> OrganizePhotoAsync(SynoFile file);
    }
    internal class PhotoService : IPhotoService
    {
        private readonly ISynologyFileService _synologyFileService;
        public PhotoService(ISynologyFileService synologyFileService) 
        {
            _synologyFileService = synologyFileService;
        }

        public Task<IEnumerable<SynoFile>> GetUnorganizedPhotosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrganizePhotoAsync(SynoFile file)
        {
            throw new NotImplementedException();
        }
    }
}
