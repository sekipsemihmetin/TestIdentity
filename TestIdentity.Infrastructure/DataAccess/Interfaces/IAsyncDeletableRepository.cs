using TestIdentity.Domain.Core.BaseEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncDeletableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
    {
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
