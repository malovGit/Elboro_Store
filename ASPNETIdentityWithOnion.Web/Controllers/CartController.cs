using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Extensions;
using ASPNETIdentityWithOnion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Controllers
{
    public class CartController : Controller
    {
        private  IShopingCartManager cartRepository;
        private  IProductManager productRepository;
        private  ICustomersManager customerRpository;
        public CartController(IShopingCartManager cartManager, IProductManager productManager, ICustomersManager customerManager)
        {
            cartRepository = cartManager;
            productRepository = productManager;
            customerRpository = customerManager;
        }
                
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddToCart(int productId, int count)
        {
            if(count < 1 || productId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product prod = productRepository.GetProductById(productId);
            CartReloadViewModel currentCart = new CartReloadViewModel();
            ShoppingCart cart;
            if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
            {
               cart = ShoppingCart.GetCart(HttpContext, cartRepository);
               currentCart.CountLineItems = cart.AddToCart(prod, count);
                currentCart.TotalCountCart = cart.TotalCountItems();
                currentCart.TotalPriceCart = cart.ComputeTotalValue();
                currentCart.deleteId = productId;
                currentCart.TotalPriceLine = currentCart.CountLineItems * prod.Price;
                currentCart.Message = Server.HtmlEncode(prod.Name) +
                    " has been added to your shopping cart.";

            }
            else
            {
                cart = ShoppingCart.GetGuestCart(HttpContext);

                currentCart.CountLineItems = cart.GuestAddItem(prod, count);
                currentCart.TotalCountCart = cart.GuestItemsCount();
                currentCart.TotalPriceCart = cart.GuestComputeTotalValue();
                currentCart.deleteId = productId;
                currentCart.TotalPriceLine = currentCart.CountLineItems * prod.Price;
                currentCart.Message = Server.HtmlEncode(prod.Name) +
                    " has been added to your shopping cart.";
            }
            var result = currentCart;
            return Json(result);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int productId, int count)
        {
            if (count < 1 || productId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product prod = productRepository.GetProductById(productId);
            CartReloadViewModel currentCart = new CartReloadViewModel();
            ShoppingCart cart;
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(this.HttpContext, cartRepository);
                currentCart.CountLineItems = cart.RemoveFromCart(prod, count);
                currentCart.deleteId = productId;
                currentCart.TotalCountCart = cart.TotalCountItems();
                currentCart.TotalPriceCart = cart.ComputeTotalValue();
                currentCart.TotalPriceLine = currentCart.CountLineItems * prod.Price;
                currentCart.Message = "One (1) " + Server.HtmlEncode(prod.Name) +
                    " has been removed from your shopping cart.";

            }
            else
            {
                cart = ShoppingCart.GetGuestCart(this.HttpContext);
                currentCart.CountLineItems = cart.GuestRemoveLine(prod, count);
                currentCart.deleteId = productId;
                currentCart.TotalCountCart = cart.GuestItemsCount();
                currentCart.TotalPriceCart = cart.GuestComputeTotalValue();
                currentCart.TotalPriceLine = currentCart.CountLineItems * prod.Price;
                currentCart.Message = "One (1) " + Server.HtmlEncode(prod.Name) +
                    " has been removed from your shopping cart.";
            }
            var result = currentCart;
            return Json(result);
        }

        
        [HttpGet]
        public ActionResult CurrentCartView()
        {
            ShoppingCart cart;
            int currentCount = 0;
            if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(HttpContext, cartRepository);
                currentCount = cart.TotalCountItems();
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(HttpContext);
                currentCount = cart.GuestItemsCount();
            }
            return Json(currentCount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            ShoppingCart cart;
            int currentCount = 0;
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(this.HttpContext, cartRepository);
                currentCount = cart.TotalCountItems();
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(this.HttpContext);
                currentCount = cart.GuestItemsCount();
            }

            ViewData["CartCount"] = currentCount;
            return PartialView("CartSummary");
        }

        [HttpPost]
        public ActionResult ChangeSelected(bool checkin, int productId)
        {
            bool result = false;
            ShoppingCart cart;
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(this.HttpContext, cartRepository);
                result = cart.ChangeIsCheckedProduct(checkin, productId);                                              
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(this.HttpContext);
                result = cart.GuestChangeIsCheckedProduct(checkin, productId);
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ShoppingCart cart;
            CartViewModel viewCart = new CartViewModel();
            if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(HttpContext, cartRepository);
                viewCart.Items = cart.GetCartItems();
                viewCart.TotalPayment = viewCart.Items != null ? viewCart.Items.Sum(e => e.Product.Price * e.Quantity) : decimal.Zero;
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(HttpContext);
               viewCart.TotalPayment = cart.GuestComputeTotalValue();
                viewCart.Items = cart.GuestGetCartItems();
                
            }
            viewCart.ReturnUrl = returnUrl;
            viewCart.SelectedAll = true;
            return View(viewCart);
            
        }
        
        

        [HttpGet]
        public ActionResult CheckIfUserAuth( string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                return RedirectToAction("CreateOrder", "Order");
                
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = returnUrl});
            }        
        }

        public PartialViewResult CartDetails()
        {           
                ShoppingCart cart;
                CartViewModel viewCart = new CartViewModel();
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    cart = ShoppingCart.GetCart(HttpContext, cartRepository);
                    viewCart.Items = cart.GetCartItems();
                    viewCart.TotalPayment = viewCart.Items != null ? viewCart.Items.Sum(e => e.Product.Price * e.Quantity) : decimal.Zero;
                }
                else
                {
                    cart = ShoppingCart.GetGuestCart(HttpContext);
                    viewCart.TotalPayment = cart.GuestComputeTotalValue();
                    viewCart.Items = cart.GuestGetCartItems();

                }
                //viewCart.ReturnUrl = returnUrl;
                //viewCart.SelectedAll = true;
                return PartialView(viewCart);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && productRepository != null)
        //    {
        //        if (productRepository != null)
        //        {
        //            productRepository.Dispose();
        //            productRepository = null;
        //        }
        //    }
        //    if (disposing && customerRpository != null)
        //    {
        //        if (customerRpository != null)
        //        {
        //            customerRpository.Dispose();
        //            customerRpository = null;
        //        }
        //    }
        //    if (disposing && cartRepository != null)
        //    {
        //        if (cartRepository != null)
        //        {
        //            cartRepository.Dispose();
        //            cartRepository = null;
        //        }
        //    }                            
        //    base.Dispose(disposing);
        //}
    }
}