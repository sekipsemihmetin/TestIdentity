using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Core.Interfaces
{
    public interface IUpdatable: ICreatable
    {
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
