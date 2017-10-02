using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels
{
    public class CustomerOrder : BaseEntity
    {
        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
