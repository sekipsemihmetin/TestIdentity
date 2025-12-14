using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Core.Interfaces
{
    public interface ICreatable: IEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
