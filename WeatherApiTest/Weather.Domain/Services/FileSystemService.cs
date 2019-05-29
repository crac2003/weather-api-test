using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Domain.Services
{
    [ExcludeFromCodeCoverage]
    public class FileSystemService : IFileSystemService
    {
        public Task<TResult> ReadAsync<TResult>(string name)
        {
            if (!File.Exists(name))
            {
                return Task.FromResult(default(TResult));
            }

            return Task.FromResult(JsonConvert.DeserializeObject<TResult>(File.ReadAllText(name)));
        }

        public Task SaveAsync<TModel>(string name, TModel model)
        {
            File.WriteAllText(name, JsonConvert.SerializeObject(model));
            return Task.CompletedTask;
        }
    }
}