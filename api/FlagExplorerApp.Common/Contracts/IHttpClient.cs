using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagExplorerApp.Common.Contracts
{
    public interface IHttpClient<TEntity> 
    {
        Task<TEntity?> GetAsync(string requestUrl);
    }
}
