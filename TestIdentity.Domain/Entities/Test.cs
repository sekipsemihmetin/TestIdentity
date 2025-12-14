using TestIdentity.Domain.Core.BaseEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Entities
{
    public class Test: BaseEntity
    {
        public string Name { get; set; }
    }
}
