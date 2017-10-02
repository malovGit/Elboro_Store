using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Extensions
{
    public partial class ShoppingCart
    {
       // private List<CartLine> lineCollection = new List<CartLine>();

        public static ShoppingCart GetGuestCart(HttpContextBase context)
        {
            
            Cart cart = (Cart)context.Session[GuestSessionKey];
            if(cart == null)
            {
                cart = new Cart();
                cart.Lines = new List<CartLine>();
                cart.CreatedDate = DateTime.Now;
                cart.ShopCartId = Guid.NewGuid().ToString();
                cart.IsUser = false;               
                context.Session[GuestSessionKey] = cart;
            }
            var shopCart = new ShoppingCart(context);
            shopCart.ShoppingCartId = cart.ShopCartId;
            shopCart.IsAutorize = false;
            
            return shopCart;                
        }

        public int GuestAddItem(Product product, int quantity)
        {
            Cart cart = (Cart)context.Session[GuestSessionKey];
            int? result = 0;
            CartLine line = cart.Lines.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (line == null)
            {
                cart.Lines.Add(line = new CartLine { Product = product, Quantity = quantity , IsChecked = true});
            }
            else { line.Quantity += quantity; }
            cart.UpdateDate = DateTime.Now;
            result = line.Quantity;
            return result ?? 0;
        }
        public int GuestRemoveLine(Product product, int quantity)
        {
            int? result = 0;
            Cart cart = (Cart)context.Session[GuestSessionKey];
            if(cart!= null)
            {
                CartLine line = cart.Lines.SingleOrDefault(p => p.Product.Id == product.Id);
                if(line != null)
                {
                    if(quantity >= line.Quantity)
                    {
                        cart.Lines.Remove(line);
                        result = 0;
                    }
                    else
                    {
                        line.Quantity -= quantity;
                        result = line.Quantity;
                    }
                    cart.UpdateDate = DateTime.Now;
                }
            }
            
            
            return result ?? 0;
        }
        public decimal GuestComputeTotalValue()
        {
            Cart cart = (Cart)context.Session[GuestSessionKey];
            decimal? total = cart.Lines.Sum(e => e.Product.Price * e.Quantity);
            return total ?? decimal.Zero;
        }
        public void GuestClear()
        {
            Cart cart = (Cart)context.Session[GuestSessionKey];
            cart.Lines.Clear();
        }
        public int GuestItemsCount()
        {
            Cart cart = (Cart)context.Session[GuestSessionKey];
            int? count = cart.Lines.Sum(s=>s.Quantity);
            return count ?? 0;
        }
        public List<CartLine> GuestGetCartItems()
        {
            Cart cart = (Cart)context.Session[GuestSessionKey];
            return cart.Lines.ToList();
        }

        public bool GuestChangeIsCheckedProduct(bool check, int productId)
        {

            Cart cart = (Cart)context.Session[GuestSessionKey];
            cart.Lines.SingleOrDefault(p => p.Product.Id == productId).IsChecked = check;            
            return true;
        }       
    }
}