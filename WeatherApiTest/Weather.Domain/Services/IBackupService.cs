using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Services
{
    public interface IBackupService
    {
        Task<TResult> ReadAsync<TResult>(string name);

        Task SaveAsync<TModel>(string name, TModel model);
    }
}
