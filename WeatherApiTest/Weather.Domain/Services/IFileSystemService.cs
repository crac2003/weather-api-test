using System.Threading.Tasks;

namespace Weather.Domain.Services
{
    public interface IFileSystemService
    {
        Task<TResult> ReadAsync<TResult>(string name);
        Task SaveAsync<TModel>(string name, TModel model);
    }

    public class FileSystemService : IFileSystemService
    {
        public Task<TResult> ReadAsync<TResult>(string name)
        {
            return 
        }

        public Task SaveAsync<TModel>(string name, TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
