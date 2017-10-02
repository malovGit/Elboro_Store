using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class HomeStoreViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}