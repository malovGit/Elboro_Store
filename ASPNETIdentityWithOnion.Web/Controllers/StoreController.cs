using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using ASPNETIdentityWithOnion.Core.Extensions;

namespace ASPNETIdentityWithOnion.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly int pageSize;
        private readonly int count_features_items;
        private readonly int count_recomended_items;
        private IProductManager manager;
        public StoreController( IProductManager manager)
        {
            this.manager = manager;
            pageSize = 4;
            count_features_items = 6;
            count_recomended_items = 6;
        }
        // GET: Store
        public ActionResult Index()
        {

            return View();
        }
        
        public ActionResult GetCategories()
        {
            List<CategoryStoreViewModel> categories = manager.GetCategories().Select(cat => new CategoryStoreViewModel
            {
                Name = cat.Name,
                CatId = cat.Id,
                Path = cat.Path
            }).ToList();
            return PartialView(categories);
        }
        
        //Load the subCategory to Index 
       [HttpGet]
        public ActionResult GetSubCut(int? id)
        {
            //if(id == null) return;

            var sub = manager.GetSubCategories()
                .Where(e => e.CategoryId == id).ToList()
                .Select(s => new SubCategoryViewModel
                {
                    SubId = s.Id,
                    CategoryId = s.CategoryId.Value,
                    Image = s.Path,
                    Name = s.Name
                }).ToList();
            return PartialView(sub);
        }

        //Load the SubCategory on search panel
        public JsonResult GetSubCategories()
        {
            var cat = (from s in manager.GetSubCategories()
                       select s.Name).ToList();
            return Json(cat, JsonRequestBehavior.AllowGet);
        }

        //Load products to view
        [HttpGet]
        public ActionResult GetProducts(int? id, int? pageIndex)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            pageIndex = pageIndex ?? 1;
            List<ProductStoreViewModel> prod = manager.GetProducts()
                .Where(p=>p.SubCategoryId == id)
                .Skip((pageIndex.Value-1)*pageSize)
                .Take(pageSize)
                    .Select(pro => new ProductStoreViewModel
                    {
                        Name = pro.Name,
                        ShortDescription = pro.Description,
                        LongDescription = pro.LongDescription,
                        Price = pro.Price,
                        Path = pro.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First(),
                        ProductID = pro.Id,
                        SubCategoryId = pro.SubCategoryId.Value  
                    }).ToList();
            int totalCount = manager.GetProducts().Where(x => x.SubCategoryId == id).Count();

           var products = ListExtensions.ToPaginatedList(prod, pageIndex.Value, pageSize, totalCount);
            //if (prod == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            return PartialView("GetProducts", products);
        }

        public ActionResult ProductIndex()
        {

            return PartialView();
        }
        //Load left menu
        public PartialViewResult LeftMenu()
        {
            LeftPanelViewModel data = new LeftPanelViewModel();

            data.Categories = manager.GetCategories().ToList();

            data.SubCategories = manager.GetSubCategories().ToList();
            foreach (var item in data.Categories)
            {
                item.SubCategories = (from c in data.SubCategories
                                      where c.CategoryId == item.Id
                                      select c).ToList();
            }
            return PartialView(data);
        }

        public ActionResult AutocompleteSearch(string term, string currentCat)
        {
            if(currentCat != null)
            {
                if(currentCat == "All")
                {
                    var items = manager.GetProducts()
                                       .Where(e => e.Name.ToUpper().Contains(term.ToUpper()))
                                       .Select(e => new { value = e.Name})
                                       .Distinct();
                    if(items != null)
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int subCatId = manager.GetSubCategories().Where(e => e.Name.ToUpper() == currentCat.ToUpper())
                                                           .Select(p => p.Id)
                                                           .First();
                    var items = manager.GetProducts()
                                       .Where(p => p.SubCategoryId == subCatId && p.Name.ToUpper().Contains(term.ToUpper()))                                       
                                       .Select(s => new { value = s.Name }).Distinct();
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
                
            }
            return null;
        }

        public ActionResult FeaturesItems()
        {             
            var features = manager.GetProducts()
                .Take(count_features_items)
                .Select(e => new ProductStoreViewModel
            {
                Name = e.Name,
                LongDescription = e.LongDescription,
                ShortDescription = e.Description,
                Price = e.Price,
                Path = e.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First(),
                ProductID = e.Id,
                SubCategoryId = e.SubCategoryId.Value
            }).ToList();

            return PartialView(features);
        }

        public PartialViewResult RecommendedItems()
        {
            
            List<ProductStoreViewModel>  recommended = manager.GetProducts()
                .Take(count_recomended_items)
                .Select(e=> new ProductStoreViewModel
            {
                Name = e.Name,
                LongDescription = e.LongDescription,
                ShortDescription = e.Description,
                Price = e.Price,
                Path = e.Images.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First(),
                ProductID = e.Id,
                SubCategoryId = e.SubCategoryId.Value
            }).ToList();
            return PartialView(recommended);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult CategoriesOfMainMenu()
        {
            CategoriesMenuViewModel cat = new CategoriesMenuViewModel();

            cat.Categories = manager.GetCategories();
                                                                                                                               
            return PartialView("CategoryMenu", cat);
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