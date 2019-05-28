using System.Threading.Tasks;

namespace Weather.Domain.Services
{
    public interface IFileSystemService
    {
        Task<TResult> ReadAsync<TResult>(string name);
        Task SaveAsync<TModel>(string name, TModel model);
    }
}
