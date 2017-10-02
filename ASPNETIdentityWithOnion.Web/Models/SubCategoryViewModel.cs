using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class SubCategoryViewModel
    {
        public int SubId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        // public virtual ICollection<Product> Products { get; set; }





    }
}