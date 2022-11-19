using BarryFamily.Synology.PhotoOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface IPhotoService
    {
        Task<IEnumerable<SynoFile>> GetUnorganizedPhotos();
        Task<bool> OrganizePhoto(SynoFile file);
    }
    internal class PhotoService : IPhotoService
    {
        public PhotoService() { }
        public Task<IEnumerable<SynoFile>> GetUnorganizedPhotos()
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrganizePhoto(SynoFile file)
        {
            throw new NotImplementedException();
        }
    }
}
