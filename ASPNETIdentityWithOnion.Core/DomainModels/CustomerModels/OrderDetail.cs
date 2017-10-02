using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels
{
    public class OrderDetail : BaseEntity
    {
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }        
        //public bool PaidOrder { get; set; }
    }
}
