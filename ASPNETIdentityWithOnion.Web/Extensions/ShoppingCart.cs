using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Extensions
{
    public partial class ShoppingCart
    {
        private readonly IShopingCartManager cartManager;
        //private IShopingCartManager Manager { get { return cartManager; } }
        private const string CartSessionKey = "CartId";
        private const string GuestSessionKey = "GuestId";
        public string ShoppingCartId;
        public bool IsAutorize { get; set; }
        private HttpContextBase context;
        public ShoppingCart(IShopingCartManager manager)
        {
            cartManager = manager;
        }
        public ShoppingCart(HttpContextBase _context)
        {
            context = _context;
        }

        //Cart Methods

        //private List<CartLine> lineCollection = new List<CartLine>();
        public static ShoppingCart GetCart(HttpContextBase context, IShopingCartManager manager)
        {
            var cart = new ShoppingCart(manager);
            cart.ShoppingCartId = cart.GetCartId(context);
            cart.IsAutorize = true;
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller, IShopingCartManager manager)
        {
            return GetCart(controller.HttpContext, manager);
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                context.Session[CartSessionKey] =
                       context.User.Identity.Name;
                //if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                //{
                //    context.Session[CartSessionKey] =
                //        context.User.Identity.Name;
                //}
                //else
                //{
                //    // Generate a new random GUID using System.Guid class
                //    Guid tempCartId = Guid.NewGuid();
                //    // Send tempCartId back to client as a cookie
                //    context.Session[CartSessionKey] = tempCartId.ToString();

                //}
            }
            return context.Session[CartSessionKey].ToString();
        }        

        public int AddToCart(Product product, int quantity)
        {

            Cart cart = cartManager.GetCarts().SingleOrDefault(c => c.ShopCartId == ShoppingCartId);

            if(cart == null)
            {
                cart = new Cart { ShopCartId = ShoppingCartId,
                    IsUser = IsAutorize,
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    TotalItems = 0
                                  
                                 // Lines = new List<CartLine>()
                };
                cartManager.CreateCart(cart);
                CartLine newline = new CartLine { ProductId = product.Id, CartId = cart.Id, Quantity = quantity, IsChecked = true };
                cartManager.CreateCartLine(newline);
                cart.TotalItems = cart.Lines.Sum(c => c.Quantity);
                cartManager.UpdateCart(cart);
                return newline.Quantity;
            }
            else
            {
                CartLine line = cart.Lines.Where(p => p.ProductId == product.Id).FirstOrDefault();
                if(line == null)
                {
                    line = new CartLine { ProductId = product.Id, CartId = cart.Id, Quantity = quantity, IsChecked = true };
                    cartManager.CreateCartLine(line);
                }
                else
                {
                    line.Quantity += quantity;
                }
                cart.UpdateDate = DateTime.Now;
                cart.TotalItems = cart.Lines.Sum(c => c.Quantity);
                cartManager.UpdateCart(cart);
                return line.Quantity;
            }
        }

        public int RemoveFromCart(Product product, int quantity)
        {
            int? result = 0;
            Cart cart = cartManager.GetCarts().SingleOrDefault(p => p.ShopCartId == ShoppingCartId);
            if(cart != null)
            {
                CartLine toRemove = cart.Lines.SingleOrDefault(c => c.ProductId == product.Id);
                if(toRemove != null)
                {
                    if(toRemove.Quantity <= quantity)
                    {
                        cartManager.DeleteCartLine(toRemove.Id);
                        result = 0;
                    }
                    else
                    {
                        toRemove.Quantity -= quantity;
                        result = toRemove.Quantity;
                    }
                    cart.UpdateDate = DateTime.Now;
                    cart.TotalItems = cart.Lines.Sum(c => c.Quantity);
                    cartManager.UpdateCart(cart);
                }              
            }            
            return result ?? 0;
        }

        //private int TotalQuantity(Cart cart)
        //{
        //    int res = 0;
        //    res = cart.Lines.Sum(c => c.Quantity);
        //    return res;

        //}

        public int TotalCountItems()
        {
            Cart cart = cartManager.GetCarts().FirstOrDefault(p => p.ShopCartId == ShoppingCartId);


            return cart != null ?  cart.TotalItems.Value : 0 ;
        }
        public List<CartLine> GetCartItems()
        {
            Cart cart = cartManager.GetCarts().SingleOrDefault(c => c.ShopCartId == ShoppingCartId);

            return cart != null ? cart.Lines.ToList() : null;                  
                    
        }

        public bool ChangeIsCheckedProduct(bool check, int productId)
        {
           
                Cart cart = cartManager.GetCarts().SingleOrDefault(c => c.ShopCartId == ShoppingCartId);
                cart.Lines.SingleOrDefault(p => p.ProductId == productId).IsChecked = check;
                cartManager.UpdateCart(cart);
            
            return true;
        }

        public decimal ComputeTotalValue()
        {
            //decimal? total = (from item in GetCartItems()
            //                  select (item.Quantity * item.Product.Price)).Sum();
            decimal? total = GetCartItems().Sum(e => e.Product.Price * e.Quantity);
            return total ?? decimal.Zero;
        }

        public void ClearCart()
        {
            Cart cart = cartManager.GetCarts().SingleOrDefault(c => c.ShopCartId == ShoppingCartId);

            foreach (var cartItem in cart.Lines.ToList())
            {
                cartManager.DeleteCartLine(cartItem.Id);
            }
            cart.Lines.Clear();
            cart.UpdateDate = DateTime.Now;
            //Save changes
            cartManager.DeleteCart(cart.Id);
        }   
    }
}