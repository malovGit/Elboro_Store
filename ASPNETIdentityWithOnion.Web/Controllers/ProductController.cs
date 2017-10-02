using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Core.Extensions;
using ASPNETIdentityWithOnion.Web.Models;
using ASPNETIdentityWithOnion.Web.Extensions;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ASPNETIdentityWithOnion.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
       // private readonly int pageSize;
        private IProductManager manager;
        public ProductController(IProductManager manager)
        {
            this.manager = manager;
            //pageSize = 4;
        }

        public ActionResult ProductList(int? subId, SortCategories sortBy = SortCategories.All, int page = 1, int pageSize = 4)
        {
            if (subId == null)
            {
                return HttpNotFound();
            }
            //if(sortBy.Value < 0 || sortBy == null)
            //{
            //    sortBy = SortCategories.All;
            //}
          
            IEnumerable<Product> pro = manager.GetProducts().Where(p => p.SubCategoryId == subId);

            //if(pro.Count() > 0 )
            //{
                switch (sortBy)
                {
                    case SortCategories.Name:
                        pro = pro.OrderBy(p => p.Name);
                        break;
                    case SortCategories.PriceHigh:
                        pro = pro.OrderByDescending(p => p.Price);
                        break;
                    case SortCategories.PriceLower:
                        pro = pro.OrderBy(p => p.Price);
                        break;
                    case SortCategories.Reviews:
                        pro.ToList();
                        break;
                    case SortCategories.BestShiping:
                        pro.ToList();
                        break;
                    default:
                        pro.ToList();
                        break;
                }

                List<ProductStoreViewModel> prod = pro
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
                   .Select(p => new ProductStoreViewModel
                   {
                       Name = p.Name,
                       ShortDescription = p.Description,
                       LongDescription = p.LongDescription,
                       Price = p.Price,
                       Path = p.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First(),
                       ProductID = p.Id,
                       SubCategoryId = p.SubCategoryId.Value,
                   }).ToList();

                int totalCount = pro.Count();

                var products = ListExtensions.ToPaginatedList(prod, page, pageSize, totalCount);

            ProductsListViewModel model = new ProductsListViewModel
            {
                ProductList = products,
                pageSizes = new List<SelectListItem>
                    {
                        new SelectListItem {Text = "4", Value = "4" },
                        new SelectListItem {Text = "8", Value = "8" },
                         new SelectListItem {Text = "12", Value = "12" },
                          new SelectListItem {Text = "24", Value = "24" },
                    },
                CurrentSubCategoryId = subId.Value,
                SubCategoryName = manager.GetSubCategoryById(subId.Value).Name,
                CurrentPage = products.PageIndex
                };

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_PartialProductList", model);
                }
                return View(model);
            //}
                
            
               // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prod = manager.GetProductById(id.Value);
            if (prod == null)
            {
                return HttpNotFound();
            }
            string[] temp = prod.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            ProductStoreViewModel product = new ProductStoreViewModel
            {
                Name = prod.Name,
                Price = prod.Price,
                ShortDescription = prod.Description,
                LongDescription = prod.LongDescription,
                ProductID = prod.Id,
                
                
                Images = (from t in temp
                         select Url.Content(ConfigurationManager.AppSettings["picPath"]
                           .ToString() + "/Product/" + t)).ToList()


            };
            return View(product);
        }

        public ActionResult SearchProduct(string searchPlace)
        {
            if(searchPlace == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var products = (from p in manager.GetProducts()
                            where p.Name.ToUpper().Contains(searchPlace.ToUpper())
                            select p).ToList();

            List<ProductStoreViewModel> prod = products.Select(p => new ProductStoreViewModel
            {
                Name = p.Name,
                Price = p.Price,
                ShortDescription = p.Description,
                LongDescription = p.LongDescription,
                SubCategoryId = p.SubCategoryId.Value,
                ProductID = p.Id,
                Path = p.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First()
            }).ToList();

            return View(prod);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && manager != null)
        //    {
        //        if (manager != null)
        //        {
        //            manager.Dispose();
        //            manager = null;
        //        }
        //    }
        //    base.Dispose(disposing);
        //}
    }
}