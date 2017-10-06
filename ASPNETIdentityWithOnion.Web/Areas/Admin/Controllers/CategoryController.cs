using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Core.Services;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using ASPNETIdentityWithOnion.Web.Extensions;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using MvcSiteMapProvider.Web.Script.Serialization;
using System.Web.Script.Serialization;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private IProductManager manager;
        //private readonly IPicturesManager pictures;
        //private readonly IService<Image> imageServise;
        public CategoryController(IProductManager manager)
        {
            this.manager = manager;
        }
        // GET: Category
        public async Task<ActionResult> Index()
        {
            var categories = await manager.GetCategoriesAsync();
            List<CategoryViewModel> cvms = new List<CategoryViewModel>();
            foreach (var item in categories)
            {
                cvms.Add(StoreExtention.ToCategoryViewModel(item));
            }
            return View(cvms);
        }

        // GET: PartialCategory
      
        public ActionResult PartialIndex()
        {
            var categories = manager.GetCategories();
            List<CategoryViewModel> cvms = new List<CategoryViewModel>();
            foreach (var item in categories)
            {
                cvms.Add(StoreExtention.ToCategoryViewModel(item));
            }
            return PartialView(cvms);
        }
        // GET: Category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await manager.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            var cvm = StoreExtention.ToCategoryViewModel(category);
            //ViewBag.Products =  (from p in category.Products
            //                          select p).ToList();
            return View(cvm);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            CategoryViewModel category = new CategoryViewModel();
            return View(category);
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var result = await manager.CreateCategoryAsync(StoreExtention.ToCategory(category));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
            }
            return View();

        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await manager.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(StoreExtention.ToCategoryViewModel(category));
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var result = await manager.UpdateCategoryAsync(StoreExtention.ToCategory(category));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Category is not updated", result.Errors.First());
                return View(category);
            }
            return View(category);
        }

        // GET: Category/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await manager.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(StoreExtention.ToCategoryViewModel(category));
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category cat = await manager.GetCategoryByIdAsync(id.Value);
                if (cat == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    string pathToDel = cat.Path;
                    string fullPath = Request.MapPath(ConfigurationManager.AppSettings["picPath"].
                        ToString() + "/Category/" + pathToDel);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    await manager.DeleteCategoryAsync(id.Value);

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile, Category cat)
        {
            if (uploadFile != null)
            {
                ViewBag.err = null;

                string filename = System.IO.Path.GetFileName(uploadFile.FileName);
                if (ImageUtils.ExistImage("~/CategoryImages/", filename))
                {
                    ViewBag.err = "This name is exist";
                    return View("Create", cat);
                }
                //save the image
                uploadFile.SaveAs(Server.MapPath("~/CategoryImages/" + filename));
                cat.Path = "~/CategoryImages/" + filename;
                return View("Create", cat);
            }
            return View("Create");
        }
       
        [HttpPost]
        public JsonResult UploadImage()
        {
            string numberImg = Request.Form[0];
            //string imgPath = null;
            string pic = null;
            //string resizePath = "";
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    pic = ImageUtils.SaveImage(upload, "Category/");
                }
            }
            string[] imageData = { numberImg, pic };
            return Json(imageData);
        }

        [HttpPost]
        public JsonResult ReloadImage()
        {
            string pathToDel = Request.Form["Path"];

            string numberImg = Request.Form[0];
           // string imgPath = null;
            string pic = null;
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {                    
                    pic = ImageUtils.SaveImage(upload, "Category/");                   
                    string fullPath = Request.MapPath(ConfigurationManager.AppSettings["picPath"].
                        ToString() + "/Category/" + pathToDel);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }
            string[] imageData = { numberImg, pic };
            return Json(imageData);
        }

        [HttpPost]
        public JsonResult DeleteImage()
        {
            string imgName = Request.Form[0];
            string delFile = Server.MapPath(imgName);
            FileInfo fileInf = new FileInfo(delFile);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                return Json(imgName + " Delete!");
            }
            return Json("Error Deleting!");
        }

        public async Task<ActionResult> DeleteModal(int? id)
        {
            if (id == null || id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await manager.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteModal", category);
        }
       
    }
}
