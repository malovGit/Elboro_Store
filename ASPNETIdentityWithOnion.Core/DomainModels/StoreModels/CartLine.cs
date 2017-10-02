using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    public class CartLine : BaseEntity
    {
       public virtual Product Product { get; set; }
       public int? ProductId { get; set; }
       public int Quantity { get; set; }
       public int? CartId { get; set; }
       public Cart Cart { get; set; }
       public bool? IsChecked { get; set; }
    }
}
