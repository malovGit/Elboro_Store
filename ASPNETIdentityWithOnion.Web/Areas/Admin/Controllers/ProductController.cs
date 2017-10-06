using System.Threading.Tasks;
using System.Web.Mvc;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using System.Net;
using ASPNETIdentityWithOnion.Web.Extensions;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.IO;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        //private readonly int pageSize;
        private IProductManager manager;

        public ProductController(IProductManager manager)
        {
            this.manager = manager;
            //pageSize = 10;
        }
        
        public async Task<ActionResult> Index()//(int pageIndex = 1)
        {
            List<ProductViewModel> pvm = new List<ProductViewModel>();
            var products = await manager.GetProductsAsync();
            int i = 0;
            foreach (Product item in products)
            {
                pvm.Add(StoreExtention.ToProductViewModel(item));
                pvm[i].SubCategoryName = manager.GetSubCategoryById(item.SubCategoryId.Value).Name;
                i++;
            }                                 
            return View(pvm);

        }

        // GET: PaertialIndex
        public ActionResult PartialIndex()
        {
            List<ProductViewModel> pvm = new List<ProductViewModel>();
            var products = manager.GetProducts();
            int i = 0;
            foreach (Product item in products)
            {
                pvm.Add(StoreExtention.ToProductViewModel(item));
                pvm[i].SubCategoryName = manager.GetSubCategoryById(item.SubCategoryId.Value).Name;
                i++;
            }
            return View(pvm);
        }

        // GET: /Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await manager.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductViewModel prod = StoreExtention.ToProductViewModel(product);
            prod.SubCategoryName = manager.GetSubCategoryById(prod.SubSelectedId).Name;
            //prod.SubCategory = await manager.GetSubCategoryByIdAsync(product.SubCategoryId);

            return View(prod);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel product = new ProductViewModel();
            product.Categories = manager.GetCategories().ToList()
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                 Text = e.Name,
                 Selected = true
            }).ToList();
            product.SubCategories = new List<SelectListItem>();
            //insert the first element to show
            //product.Categories.Insert(0, new SelectListItem
            //{
            //    Value = "-1",
            //    Text = "Categories"
            //});
            return View(product);
        }
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {                                 
                Product prod = StoreExtention.ToProduct(product);
                var result = await manager.CreateProductAsync(prod);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View(product);
                    }                                             
            }
            return View();
        }

        // GET: /Product/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.GetProductById(id.Value);
            ProductViewModel pvm = StoreExtention.ToProductViewModel(product);
            pvm.SelectedId = manager.GetSubCategoryById(pvm.SubSelectedId).CategoryId.Value;
            pvm.SubCategoryName = manager.GetSubCategoryById(pvm.SubSelectedId).Name;
            pvm.Categories = manager.GetCategories().ToList()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name,
                    Selected = pvm.SelectedId == e.Id
                }).ToList();
            pvm.SubCategories = manager.GetSubCategories().ToList()
                .Where(e => e.CategoryId == pvm.SelectedId).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = pvm.SubSelectedId == c.Id
                }).ToList();
            //pvm.SubCategories = new List<SelectListItem>();
            //pvm.SubCategories.Add(new SelectListItem
            //{
            //    Value = pvm.SubSelectedId.ToString(),
            //    Text = pvm.SubCategoryName,
            //    Selected = true
            //});

            
            return View(pvm);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var prod = await manager.GetProductByIdAsync(product.ProductID);
                
                if (prod == null)
                {
                    return HttpNotFound();
                }
                prod.SubCategoryId = product.SelectedId;
               // prod.SubCategory = product.SubCategory;
                if(product.Images != null)
                {
                    prod.Images = product.Path;
                }
                
                prod.IsActive = product.IsActive;
                prod.Description = product.ShortDescription;
                prod.LongDescription = product.LongDescription;
                prod.Name = product.Name;
                prod.Price = product.Price;

                var result = await manager.UpdateProductAsync(prod);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Update Product", result.Errors.First());
                return View();
            }
            ModelState.AddModelError("Update Product", "Something failed.");
            return View();
        }

        // GET: /Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await manager.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            var prod = StoreExtention.ToProductViewModel(product);
            prod.SubCategoryName = manager.GetSubCategoryById(prod.SubSelectedId).Name;
            
            return View(prod);
        }
        
        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ProductViewModel prod)
        {
            if (ModelState.IsValid)
            {
                if (prod == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (prod.Path != null)
                {

                    char[] delim = { ',' };
                    string[] res = prod.Path.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in res)
                    {
                        string fullPath = Request.MapPath(ConfigurationManager.AppSettings["picPath"].
                        ToString() + "/Product/" + item);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                    }
                }         
                var result = await manager.DeleteProductAsync(prod.ProductID);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Delete Product", result.Errors.First());
                    return View();
                }  
                               
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }
        [HttpPost]
        public JsonResult CreateImage()
        {
            
           // string imgPath = null;
            string imgName = null;
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    imgName = ImageUtils.SaveImage(upload, "Product/");                    
                }
            }
            string[] imageData = { imgName };
            return Json(imageData);
        }


        [HttpPost]
        public JsonResult UploadImage()
        {
            string prodId = Request.Form[0];           
            //string imgPath = null;
            string imgName = null;
            Product prod = manager.GetProductById(Int32.Parse(prodId));
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {                   
                    imgName = ImageUtils.SaveImage(upload, "Product/");                   
                    prod.Images += imgName + ",";
                }
            }
            var imgs = prod.Images;
            manager.UpdateProduct(prod);

            string[] imageData = { imgName};
            return Json(imageData);
        }

        public ActionResult ShowImages(int id)
        {
            string img = manager.GetProductById(id).Images;
            char[] delim = { ',' };
            string[] res = img.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            List<string>images = new List<string>(res.ToList());
            if (images != null)
            {
                return PartialView(images);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public JsonResult DeleteImage()
        {
            string prodId = Request.Form[1];
            string picToDel = System.IO.Path.GetFileName(Request.Form[0]);
            string imgName = Request.Form[0];
            string delFile = Server.MapPath(imgName);
            FileInfo fileInf = new FileInfo(delFile);

            Product pro = manager.GetProductById(Int32.Parse(prodId));
            if(pro.Images.Contains(picToDel + ","))
            {
                pro.Images = pro.Images.Replace(picToDel+",", "");
            }
            else
            {
                pro.Images = pro.Images.Replace(picToDel, "");
            }
            string images = pro.Images;
            var result = manager.UpdateProduct(pro);
            if (result.Succeeded)
            {
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                    return Json(images);
                }
                ModelState.AddModelError("", result.Errors.First());
                return Json("Error Deleting!");
            }
            ModelState.AddModelError("", "Something failed.");
            return Json("Error Deleting!");
        }

        [HttpPost]
        public JsonResult DelImg()
        {
            string numImg = Request.Form[0];
            string fileName = Request.Form[1];
            var path = Server.MapPath("~/Files/" + fileName);
            FileInfo finf = new FileInfo(path);
            if (finf.Exists)
            {
                finf.Delete();
                return Json("Вы успешно удалили файл: " + fileName);
            }
            else
            {
                return Json("Не удалось удалить файл: " + fileName);
            }

        }

        [HttpGet]
        public ActionResult SelectSubCat(int id)
        {
            return Json(manager.GetSubCategories().Where(p => p.CategoryId == id)
                .ToList().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}