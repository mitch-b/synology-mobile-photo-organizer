using BarryFamily.Synology.PhotoOrganizer.Models;

namespace BarryFamily.Synology.PhotoOrganizer.Services
{
    internal interface ITokenizedFilePathService
    {
        string? GetTokenizedFilePath(string filePath, SynoFile file);
    }

    internal class TokenizedFilePathService : ITokenizedFilePathService
    {
        public TokenizedFilePathService() { }

        public string? GetTokenizedFilePath(string filePath, SynoFile file)
        {
            if (filePath == null) return null;
            if (file == null) return filePath;

            return filePath
                .Replace("<year>", file.Date.Year.ToString())
                .Replace("<month>", file.Date.ToString("MM"))
                .Replace("<day>", file.Date.ToString("dd"));
        }
    }
}
