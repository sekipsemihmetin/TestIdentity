using TestIdentity.Domain.Entities;
using TestIdentity.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.Repositories.TestRepositories
{
    public interface ITestRepository:IAsyncInsertable<Test>,IAsyncDeletableRepository<Test>,IAsyncFindableRepository<Test>,IAsyncQueryableRepository<Test>,IAsyncUpdatableRepository<Test>,IAsyncOrderableRepository<Test>
    {
    }
}
