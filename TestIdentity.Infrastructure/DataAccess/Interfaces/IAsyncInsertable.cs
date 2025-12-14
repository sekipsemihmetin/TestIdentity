using TestIdentity.Domain.Core.BaseEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncInsertable<TEntity>: IAsyncRepository where TEntity : BaseEntity
    {
        Task<TEntity> AddAsnyc(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
