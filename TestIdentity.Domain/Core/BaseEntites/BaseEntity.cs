using TestIdentity.Domain.Core.Interfaces;
using TestIdentity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Domain.Core.BaseEntites
{
    public class BaseEntity : IUpdatable
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
