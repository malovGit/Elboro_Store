using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    [Serializable]
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public string Path { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
    }
}
