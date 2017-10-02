using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.ProductModels
{
    public class Description : BaseEntity
    {
       public virtual Dictionary<string,string> Descriptions { get; set; }
    }
}
