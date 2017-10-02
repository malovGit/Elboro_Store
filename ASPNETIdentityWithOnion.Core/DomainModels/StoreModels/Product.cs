using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    public class Product : BaseEntity
    {

        public virtual string Name { get; set; }

        [Display(Name="Description")]
        public virtual string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string LongDescription { get; set; }

        public virtual decimal Price { get; set; }

        [Display(Name ="Activate")]
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public bool IsOrdered { get; set; }

        [ScaffoldColumn(false)]
        public bool InStock { get; set; }

        public string Images { get; set; }

        public int? SubCategoryId { get; set; }

        public string Color { get; set; }

        public float Size { get; set; }

        public virtual SubCategory SubCategory { get; set; } 
    }
}
