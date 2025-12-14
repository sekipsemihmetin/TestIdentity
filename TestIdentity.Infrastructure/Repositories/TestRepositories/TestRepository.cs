using TestIdentity.Domain.Entities;
using TestIdentity.Infrastructure.AppContext;
using TestIdentity.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.Repositories.TestRepositories
{
    public class TestRepository: EFBaseRepository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext context ): base(context)
        { 
            
        }

    }
}
