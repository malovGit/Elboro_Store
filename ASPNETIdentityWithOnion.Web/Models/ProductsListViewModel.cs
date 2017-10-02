using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class ProductsListViewModel
    {
        public List<SelectListItem> pageSizes { get; set; }
        public PaginatedList<ProductStoreViewModel> ProductList { get; set; }
        public int SelectedPageSize { get; set; }
        public int CurrentSubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CurrentPage { get; set; }
    }
}