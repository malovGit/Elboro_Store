using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using ASPNETIdentityWithOnion.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin, Manager, Anonim")]
    public class SubCategoryController : Controller
    {
        private IProductManager manager;

        public SubCategoryController(IProductManager manager)
        {
            this.manager = manager;
        }
        // GET: Admin/SubCategory
        public ActionResult Index()
        {
            List<SubCategoryViewModel> list = new List<SubCategoryViewModel>();
            int i = 0;
            foreach (var item in manager.GetSubCategories())
            {
                list.Add(StoreExtention.ToSubCategoryViewModel(item));
                list[i].CategoryName = manager.GetCategoryById(list[i].SelectedId).Name;
                i++;
            }
            return View(list);
        }
        // GET: PaertialIndex
        public ActionResult PartialIndex()
        {
            List<SubCategoryViewModel> list = new List<SubCategoryViewModel>();
            int i = 0;
            foreach (var item in manager.GetSubCategories())
            {
                list.Add(StoreExtention.ToSubCategoryViewModel(item));
                list[i].CategoryName = manager.GetCategoryById(list[i].SelectedId).Name;
                i++;
            }
            return View(list);
        }

        // GET: Admin/SubCategory/Details/5
        public ActionResult Details(SubCategoryViewModel subCategory)
        {
            if (subCategory == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(subCategory);
        }

        // GET: Admin/SubCategory/Create
        public ActionResult Create()
        {
            SubCategoryViewModel scvm = new SubCategoryViewModel();
            scvm.Categories = manager.GetCategories();
            return View(scvm);
        }

        // POST: Admin/SubCategory/Create
        [HttpPost]
        public ActionResult Create(SubCategoryViewModel subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.CategoryName = manager.GetCategoryById(subCategory.SelectedId).Name;
                SubCategory sub = StoreExtention.ToSubCategory(subCategory);
                var result = manager.CreateSubCategory(sub);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Create SubCategory", result.Errors.First());
                    return View();
                }
            }
            return View();

        }

        // GET: Admin/SubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = manager.GetSubCategoryById(id.Value);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            SubCategoryViewModel scvm = StoreExtention.ToSubCategoryViewModel(subCategory);
            scvm.Categories = manager.GetCategories();
            return View(scvm);
        }

        // POST: Admin/SubCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(SubCategoryViewModel subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.CategoryName = manager.GetCategoryById(subCategory.SelectedId).Name;
                var result = manager.UpdateSubCategory(StoreExtention.ToSubCategory(subCategory));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Edit subcategory", result.Errors.First());
                return View();
            }
            return View();
        }

        // GET: Admin/SubCategory/Delete/5
        public ActionResult Delete(SubCategoryViewModel subCategory)
        {
            if (subCategory == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(subCategory);
        }

        // POST: Admin/SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = manager.GetSubCategoryById(id.Value);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                string pathToDel = subCategory.Path;
                string fullPath = Request.MapPath(ConfigurationManager.AppSettings["picPath"].
                    ToString() + "/SubCategory/" + pathToDel);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                manager.DeleteSubCategory(id.Value);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            string numberImg = Request.Form[0];
           // string imgPath = null;
            string pic = null;
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    pic = ImageUtils.SaveImage(upload, "Subcategory/");
                    //imgPath = System.IO.Path.Combine(ConfigurationManager.AppSettings["picPath"].
                    //    ToString() + "Subcategory/", pic);
                    //upload.SaveAs(Server.MapPath(imgPath));
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
                return Json(imgName + " Deleted!");
            }
            return Json("Error Deleting!");
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
