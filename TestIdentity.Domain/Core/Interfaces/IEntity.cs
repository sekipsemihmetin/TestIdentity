using TestIdentity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Core.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public Status Status{ get; set; }
    }
}
