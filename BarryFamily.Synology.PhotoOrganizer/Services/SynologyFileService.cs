using BarryFamily.Synology.PhotoOrganizer.Models;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using Microsoft.Extensions.Options;
using Synology.Api.Client;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface ISynologyFileService
    {
        Task<IEnumerable<SynoFile>> GetFilesAsync(string path);
        Task<bool> MoveFileAsync(string sourcePath, string destinationPath, bool createMissingFolders = true);
        Task<bool> CreateFolderAsync(string path);
    }
    internal class SynologyFileService : ISynologyFileService
    {
        private readonly ISynologyClient _synologyClient;
        private readonly SynologyConnection _synologyConnection;
        public SynologyFileService(IOptions<SynologyConnection> synologyConnectionOptions)
        {
            _synologyConnection = synologyConnectionOptions.Value;
            //_synologyClient = new SynologyClient(_synologyConnection.Host);
        }
        public Task<bool> CreateFolderAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SynoFile>> GetFilesAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveFileAsync(string sourcePath, string destinationPath, bool createMissingFolders = true)
        {
            throw new NotImplementedException();
        }
    }
}
