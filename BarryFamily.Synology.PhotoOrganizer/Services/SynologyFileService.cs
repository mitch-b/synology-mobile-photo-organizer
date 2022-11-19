using BarryFamily.Synology.PhotoOrganizer.Models;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using Microsoft.Extensions.Options;
using Synology.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public SynologyFileService(IOptions<SynologyConnection> synologyConnectionOptions)
        {
            _synologyClient = new SynologyClient("");
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
