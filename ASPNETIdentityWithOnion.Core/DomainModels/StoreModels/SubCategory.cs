using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    [Serializable]
    public class SubCategory : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual string Path { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public SubCategory()
        {
            Products = new List<Product>();
        }
    }
}
