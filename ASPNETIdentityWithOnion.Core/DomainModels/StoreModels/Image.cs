using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels.StoreModels
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }

        //public virtual string Name { get; set; }
        //public virtual byte[] Image { get; set; }
        //public virtual string Mimetype { get; set; }
        //public int? Height { get; set; }
        //public int? Width { get; set; }
        //public virtual Product Product { get; set; }
    }
}
