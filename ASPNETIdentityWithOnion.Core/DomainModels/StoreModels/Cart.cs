using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    public class Cart : BaseEntity
    {
        public virtual ICollection<CartLine> Lines { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ShopCartId { get; set; }
        public bool IsUser { get; set; }
        public int? TotalItems { get; set; }
        public Cart()
        {
            Lines = new List<CartLine>();
        }
    }
}
