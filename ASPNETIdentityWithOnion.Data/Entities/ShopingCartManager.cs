using ASPNETIdentityWithOnion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System.Linq.Expressions;

namespace ASPNETIdentityWithOnion.Data.Entities
{
    public class ShopingCartManager : IShopingCartManager
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private IRepository<Cart> carts;
        private IRepository<CartLine> lines;
        private bool disposed;
       

        public ShopingCartManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            carts = UnitOfWork.Repository<Cart>();
            lines = UnitOfWork.Repository<CartLine>();
        }

       

        public ApplicationResult CreateCart(Cart cart)
        {
            ApplicationResult result;
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProduct" }, false);

            }
            else
            {
                carts.Insert(cart);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateCartAsync(Cart cart)
        {
            ApplicationResult result;
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProductAsync" }, false);

            }
            else
            {
                carts.Insert(cart);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult DeleteCart(int Id)
        {
            ApplicationResult result;
            Cart cart = carts.GetSingle(Id);
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategory" }, false);

            }
            else
            {
                carts.Delete(cart);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteCartAsync(int Id)
        {
            ApplicationResult result;
            Cart cart = await carts.GetSingleAsync(Id);
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategoryAsync" }, false);
            }
            else
            {
                carts.Delete(cart);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateCart(Cart cart)
        {
            ApplicationResult result;
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategory" }, false);

            }
            else
            {
                carts.Update(cart);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateCartAsync(Cart cart)
        {
            ApplicationResult result;
            if (cart == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategoryAsync" }, false);

            }
            else
            {
                carts.Update(cart);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return carts.GetAll();
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
            return await carts.GetAllAsync();
        }

        public Cart GetCartById(int Id)
        {
            return carts.GetSingle(Id);
        }

        public async Task<Cart> GetCartByIdAsync(int Id)
        {
            return await carts.GetSingleAsync(Id);
        }       

        public async Task<ApplicationResult> CartExistsAsync(int Id)
        {
            ApplicationResult result;
            if (await carts.FindByAsync(e => e.Id == Id) == null)
            {
                result = new ApplicationResult(new List<string> { "Category not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public Cart GetCartIncluding(int Id, params Expression<Func<Cart, object>>[] propertiesIncluded)
        {
            return carts.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<Cart>  GetCartIncludingAsync(int Id, params Expression<Func<Cart, object>>[] propertiesIncluded)
        {
            return await carts.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<Cart> FindCartBy(Expression<Func<Cart, bool>> predicate)
        {
            var cart = carts.FindBy(predicate);
            if(cart != null)
            {
                return cart;
            }
            return null;
        }
        public async Task<List<Cart>> FindCartByAsync(Expression<Func<Cart, bool>> predicate)
        {
            var cart = await carts.FindByAsync(predicate);
            if (cart != null)
            {
                return cart;
            }
            return null;
        }

        //CartLine
        public ApplicationResult CreateCartLine(CartLine cartLine)
        {
            ApplicationResult result;
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateLine" }, false);

            }
            else
            {
                lines.Insert(cartLine);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateCartLineAsync(CartLine cartLine)
        {
            ApplicationResult result;
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateLineAsync" }, false);

            }
            else
            {
                lines.Insert(cartLine);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult DeleteCartLine(int Id)
        {
            ApplicationResult result;
            CartLine cartLine = lines.GetSingle(Id);
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteLine" }, false);

            }
            else
            {
                lines.Delete(cartLine);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteCartLineAsync(int Id)
        {
            ApplicationResult result;
            CartLine cartLine = await lines.GetSingleAsync(Id);
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteLineAsync" }, false);
            }
            else
            {
                lines.Delete(cartLine);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateCartLine(CartLine cartLine)
        {
            ApplicationResult result;
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateLine" }, false);

            }
            else
            {
                lines.Update(cartLine);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateCartLineAsync(CartLine cartLine)
        {
            ApplicationResult result;
            if (cartLine == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateLineAsync" }, false);

            }
            else
            {
                lines.Update(cartLine);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public IEnumerable<CartLine> GetLines()
        {
            return lines.GetAll();
        }

        public async Task<IEnumerable<CartLine>> GetLinesAsync()
        {
            return await lines.GetAllAsync();
        }

        public CartLine GetCartLineById(int Id)
        {
            return lines.GetSingle(Id);
        }

        public async Task<CartLine> GetCartLineByIdAsync(int Id)
        {
            return await lines.GetSingleAsync(Id);
        }

        public async Task<ApplicationResult> CartLineExistsAsync(int Id)
        {
            ApplicationResult result;
            if (await lines.FindByAsync(e => e.Id == Id) == null)
            {
                result = new ApplicationResult(new List<string> { "Lines not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public CartLine GetCartLineIncluding(int Id, params Expression<Func<CartLine, object>>[] propertiesIncluded)
        {
            return lines.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<CartLine> GetCartLineIncludingAsync(int Id, params Expression<Func<CartLine, object>>[] propertiesIncluded)
        {
            return await lines.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<CartLine> FindCartLineBy(Expression<Func<CartLine, bool>> predicate)
        {
            var line = lines.FindBy(predicate);
            if (line != null)
            {
                return line;
            }
            return null;
        }
        public async Task<List<CartLine>> FindCartLineByAsync(Expression<Func<CartLine, bool>> predicate)
        {
            var line = await lines.FindByAsync(predicate);
            if (line != null)
            {
                return line;
            }
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                UnitOfWork.Dispose();
            }
            disposed = true;
        }

    }
}
