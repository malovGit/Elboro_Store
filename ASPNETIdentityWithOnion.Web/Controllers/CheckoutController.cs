using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Configuration;
using ASPNETIdentityWithOnion.Web.Extensions;
using ASPNETIdentityWithOnion.Web.Models;
using RestSharp;
using RestSharp.Authenticators;
//using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private IShopingCartManager cartManager;
        private ICustomersManager customerManager;
        private IProductManager productManager;
        private IOrderManager orderManager;

        AppConfigurations appConfig = new AppConfigurations();

        public CheckoutController(IShopingCartManager cartRepository, IProductManager productRepository, ICustomersManager customerRepository, IOrderManager orderRepository)
        {
            customerManager = customerRepository;
            cartManager = cartRepository;
            productManager = productRepository;
            orderManager = orderRepository;
            
        }

        // GET: Order
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult AddressAndPayment()
        {

            //ViewData["Pay"] = new PaymentOptions();
            //ViewData["Deliv"] = new DeliveryOptions();
            ShoppingCart cart;
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(HttpContext, cartManager);
                if(cart.TotalCountItems() > 0)
                {
                    //var previusOrder = orderManager.GetOrders().FirstOrDefault(o => o.UserName == User.Identity.GetUserName());
                    var customer = customerManager.GetCustomers().SingleOrDefault(c => c.UserName == User.Identity.Name);
                    //if(previusOrder != null)                
                    // return View(previusOrder);
                    if (customer != null)
                    {
                        Order order = new Order
                        {
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Address = customer.Address,
                            City = customer.City,
                            Country = customer.Country,
                            Region = customer.Region,
                            PostalCode = customer.PostalCode,
                            Phone = customer.PhoneNumber,
                            CustomerId = customer.Id,
                            Email = customer.Email,
                            Fax = customer.Fax,
                            UserName = customer.UserName
                        };
                        return View(order);
                    }
                    return View();
                }                
                else
                {
                    return RedirectToAction("NoItems");
                }
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(HttpContext);
                if(cart.GuestItemsCount() > 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("NoItems");
                }         
               
            }
        }

        
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
                {                
                    order.UserName = User.Identity.Name;
                    order.Email = User.Identity.Name;
                    order.SaveInfo = false;
                    //credit card info
                    order.Experation = DateTime.Now;
                    order.CcType = "00000000000";
                    order.CreditCard = "Master";
                    var customer = customerManager.GetCustomers().SingleOrDefault(c => c.UserName == User.Identity.Name);
                    if(customer != null)
                    {
                        order.CustomerId = customer.Id;
                        order = CreateOrder(order);
                        customer.Orders.Add(order);
                        await customerManager.UpdateCustomerAsync(customer);
                    }
                    else
                    {
                        customer = new Customer
                        {
                            FirstName = order.FirstName,
                            LastName = order.LastName,
                            Address = order.Address,
                            Country = order.Country,
                            City = order.City,
                            PhoneNumber = order.Phone,
                            Region = order.Region,
                            PostalCode = order.PostalCode,
                            UserId = User.Identity.GetUserId(),
                            UserName = User.Identity.GetUserName()                           
                        };
                        await customerManager.CreateCustomerAsync(customer);
                        order.CustomerId = customer.Id;
                        order = CreateOrder(order);
                        customer.Orders.Add(order);
                        await customerManager.UpdateCustomerAsync(customer);
                    }
                }
            else //Guest create customer and order
            {               
                if (order.SaveInfo == true)
                {
                    Customer newCustomer = new Customer
                    {
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        Address = order.Address,
                        Country = order.Country,
                        City = order.City,
                        PhoneNumber = order.Phone,
                        Region = order.Region,
                        PostalCode = order.PostalCode

                    };
                    await customerManager.CreateCustomerAsync(newCustomer);

                        order.CustomerId = customerManager.FindCustomerBy(c => c.LastName == newCustomer.LastName 
                                                    && c.PhoneNumber == newCustomer.PhoneNumber).FirstOrDefault().Id;
                        order.Experation = DateTime.Now;
                        order.CcType = "00000000000";
                        order.CreditCard = "Master";
                        order = CreateOrder(order);
                    newCustomer.Orders.Add(order);
                        await customerManager.UpdateCustomerAsync(newCustomer);
                }
                else
                {
                        order = CreateOrder(order);
                }
            }
                // CheckoutController.SendOrderMessage(order.FirstName, "New Order: " + order.Id, order.ToString(order), appConfig.OrderEmail);

                return RedirectToAction("Complete",
                    new { id = order.Id});
           }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        
        // GET: /Checkout/Complete
        public async Task<ActionResult> Complete(int id)
        {

            // Validate customer owns this order
            var result = await orderManager.OrderExistsAsync(id);
            if(result.Succeeded )
            {
                return View(id);
            }
            else
            {
                //ModelState.AddModelError("Complete Order", result.Errors.First());
                return View("Error");
            }
        }

        [NonAction]
        public Order CreateOrder(Order order)
        {
            List<CartLine> cartItems;
            ShoppingCart cart;
            decimal orderTotal = 0;
            if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
            {
                cart = ShoppingCart.GetCart(HttpContext, cartManager);
                cartItems = cart.GetCartItems();
                order.OrderDate = DateTime.Now;
                order.ShippedDate = DateTime.Now;
                
                order.Total = 0;
               
                orderManager.CreateOrder(order);
                //order = orderManager.FindOrderBy(e => e.Email.Equals(order.Email) && e.UserName == order.UserName).FirstOrDefault();
            }
            else
            {
                cart = ShoppingCart.GetGuestCart(HttpContext);
                cartItems = cart.GuestGetCartItems();
                order.OrderDate = DateTime.Now;
                order.ShippedDate = DateTime.Now;
                orderManager.CreateOrder(order);
                order = orderManager.FindOrderBy(o => o.Phone.Equals(order.Phone) && o.LastName.Equals(order.LastName)).FirstOrDefault();

            }

            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Quantity,
                    //PaidOrder = false                  
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Quantity * item.Product.Price);
                orderManager.CreateOrderDetail(orderDetail);
                order.OrderDetails.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
            order.PaidOrder = false;

            // Save the order
            orderManager.UpdateOrder(order);
            // Empty the shopping cart
            if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                cart.ClearCart();
            else cart.GuestClear();

            // Return the OrderId as the confirmation number
            return order;
        }

        public ActionResult NoItems()
        {
            return View();
        }

        private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
        {
            RestClient client = new RestClient();
            //fix this we have this up top too
            AppConfigurations appConfig = new AppConfigurations();
            client.BaseUrl = new Uri( "https://api.mailgun.net/v2");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              appConfig.EmailApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                appConfig.DomainForApiKey, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
            request.AddParameter("to", toName + " <" + destination + ">");
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            IRestResponse executor = client.Execute(request);
            return executor as RestResponse;
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && productManager != null)
        //    {
        //        if (productManager != null)
        //        {
        //            productManager.Dispose();
        //            productManager = null;
        //        }
        //    }
        //    if (disposing && customerManager != null)
        //    {
        //        if (customerManager != null)
        //        {
        //            customerManager.Dispose();
        //            customerManager = null;
        //        }
        //    }
        //    if (disposing && cartManager != null)
        //    {
        //        if (cartManager != null)
        //        {
        //            cartManager.Dispose();
        //            cartManager = null;
        //        }
        //    }
        //    if (disposing && orderManager != null)
        //    {
        //        if (orderManager != null)
        //        {
        //            orderManager.Dispose();
        //            orderManager = null;
        //        }
        //    }
        //    base.Dispose(disposing);
        //}
    }
}