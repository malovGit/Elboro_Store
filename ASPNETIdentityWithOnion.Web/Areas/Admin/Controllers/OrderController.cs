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

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private
            IOrderManager manager;

        public OrderController(IOrderManager repository)
        {
            manager = repository;
        }


        // GET: Admin/Order
        public ActionResult Index()
        {
            var orders = (from item in manager.GetOrders()
                          select Extensions.StoreExtention.ToOrderViewModel(item)).ToList();
            return View(orders);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order =  Extensions.StoreExtention.ToOrderViewModel(manager.GetOrderById(id.Value));
            if(order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult ListOrderDetails(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<OrderDetail> details = (from it in manager.GetOrderById(orderId.Value).OrderDetails
                           select it).ToList();
            if(details == null)
            {
                return HttpNotFound();
            }
            return PartialView(details);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = Extensions.StoreExtention.ToOrderEditViewModel(manager.GetOrderById(id.Value));
            if(order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderEditViewModel order)
        {
            if (ModelState.IsValid)
            {
                Order ord = await manager.GetOrderByIdAsync(order.OrderId);
                ord = Extensions.StoreExtention.FromEditOrder(order, ord);
                var result = await manager.UpdateOrderAsync(ord);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Order not updated", result.Errors.First());
                return View(order);
            }
            return View(order);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await manager.GetOrderByIdAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
             
            return View(Extensions.StoreExtention.ToOrderViewModel(order));
        }

        // POST: /Product/Delete/5
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
                var result = await manager.DeleteOrderAsync(id.Value);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Order is not delete", result.Errors.First());
                    return View();
                }

            }
            ModelState.AddModelError("orderDelete", "order is not delete");
            return View();
        }


        public ActionResult ShippedOrders()
        {
            var orders = (from o in manager.GetOrders()
                          where o.PaidOrder.Equals(true)
                          select Extensions.StoreExtention.ToOrderViewModel(o)).ToList();
            
            return View(orders);
        }

        public ActionResult ShippedDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = Extensions.StoreExtention.ToOrderViewModel(manager.GetOrderById(id.Value));
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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