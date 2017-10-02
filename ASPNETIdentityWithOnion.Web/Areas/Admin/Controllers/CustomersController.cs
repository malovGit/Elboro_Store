using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private ICustomersManager manager;

        public CustomersController(ICustomersManager repository)
        {
            manager = repository;
        }


        // GET: Admin/Customers
        public async Task<ActionResult> Index()
        {
            var customers = await manager.GetCustomersAsync();
            List<CustomerViewModel> model = (from c in customers
                                       select Extensions.CustomerExtentions.ToCustomerViewModel(c)).ToList();
            return View(model);
        }

        // GET: PartialIndex
        public ActionResult PartialIndex()
        {
            var customers =  manager.GetCustomers();
            List<CustomerViewModel> model = (from c in customers
                                             select Extensions.CustomerExtentions.ToCustomerViewModel(c)).ToList();
            return View(model);
        }

        // GET: Admin/Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = await manager.GetCustomerByIdAsync(id.Value);
                if(customer == null)
                {
                    return HttpNotFound();
                }
                
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        [HttpPost]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = Extensions.CustomerExtentions.ToCustomer(model);
                customer.UpdateDate = DateTime.Now.ToLongDateString();
               
                var result = await manager.CreateCustomerAsync(customer);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Customer is not created", result.Errors.First());
                return View(model);

            }
            return View(model);
           
        }

        // GET: Admin/Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = await manager.GetCustomerByIdAsync(id.Value);
            if(customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel model = Extensions.CustomerExtentions.ToCustomerViewModel(customer);

            return View(model);
        }

        // POST: Admin/Customers/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, CustomerViewModel model)
        {
            Customer customer = await manager.GetCustomerByIdAsync(id.Value);
            if(customer == null)
            {
                return HttpNotFound();
            }
            customer = Extensions.CustomerExtentions.ToCustomer(model, customer);
            customer.UpdateDate = DateTime.Now.ToLongDateString();
            var result = await manager.UpdateCustomerAsync(customer);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = await manager.GetCustomerByIdAsync(id.Value);
            if(customer == null)
            {
                return HttpNotFound();
            }

            return View(Extensions.CustomerExtentions.ToCustomerViewModel( customer));
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCnfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = await manager.DeleteCustomerAsync(id.Value);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                    
                }
                ModelState.AddModelError("Customer is not deleted", result.Errors.First());
                return View();
            }
            return View();
        }

        public ActionResult TestTable()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetCustomerTable()
        {
          
            var datas = (from n in manager.GetCustomers()
                         select Extensions.CustomerExtentions.ToCustomerViewModel(n)).ToList();
            
            //JavaScriptSerializer serialaizer = new JavaScriptSerializer();
            //var json = serialaizer.Serialize(datas);
            return Json(datas, JsonRequestBehavior.AllowGet);
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
