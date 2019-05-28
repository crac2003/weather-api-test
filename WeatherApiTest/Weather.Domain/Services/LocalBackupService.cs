using System.IO;
using System.Threading.Tasks;

namespace Weather.Domain.Services
{
    public class LocalBackupService : IBackupService
    {
        private readonly ILocalBackupConfig _config;
        private readonly IFileSystemService _fileSystemService;

        public LocalBackupService(ILocalBackupConfig config, IFileSystemService fileSystemService)
        {
            _config = config;
            _fileSystemService = fileSystemService;
        }

        public Task<TResult> ReadAsync<TResult>(string name)
        {
            return _fileSystemService.ReadAsync<TResult>(Path.Combine(_config.BackupFolder, name));
        }

        public Task SaveAsync<TModel>(string name, TModel model)
        {
            return _fileSystemService.SaveAsync(Path.Combine(_config.BackupFolder, name), model);
        }
    }
}