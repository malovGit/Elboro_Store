using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.Entities
{
    public interface IShopingCartManager : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<Cart> GetCarts();
        Task<IEnumerable<Cart>> GetCartsAsync();

        ApplicationResult UpdateCart(Cart cart);
        Task<ApplicationResult> UpdateCartAsync(Cart cart);

        Task<ApplicationResult> CreateCartAsync(Cart cart);
        ApplicationResult CreateCart(Cart cart);

        Task<ApplicationResult> DeleteCartAsync(int Id);
        ApplicationResult DeleteCart(int Id);

        Cart GetCartById(int Id);
        Task<Cart> GetCartByIdAsync(int Id);

        Task<ApplicationResult> CartExistsAsync(int Id);

        Cart GetCartIncluding(int Id, params System.Linq.Expressions.Expression<Func<Cart, object>>[] propertiesIncluded);
        Task<Cart> GetCartIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<Cart, object>>[] propertiesIncluded);

        List<Cart> FindCartBy(System.Linq.Expressions.Expression<Func<Cart, bool>> predicate);
        Task<List<Cart>> FindCartByAsync(System.Linq.Expressions.Expression<Func<Cart, bool>> predicate);

        //CartLine
        IEnumerable<CartLine> GetLines();
        Task<IEnumerable<CartLine>> GetLinesAsync();

        ApplicationResult UpdateCartLine(CartLine cartLine);
        Task<ApplicationResult> UpdateCartLineAsync(CartLine cartLine);

        Task<ApplicationResult> CreateCartLineAsync(CartLine cartLine);
        ApplicationResult CreateCartLine(CartLine cartLine);

        Task<ApplicationResult> DeleteCartLineAsync(int Id);
        ApplicationResult DeleteCartLine(int Id);

        CartLine GetCartLineById(int Id);
        Task<CartLine> GetCartLineByIdAsync(int Id);

        Task<ApplicationResult> CartLineExistsAsync(int Id);

        CartLine GetCartLineIncluding(int Id, params System.Linq.Expressions.Expression<Func<CartLine, object>>[] propertiesIncluded);
        Task<CartLine> GetCartLineIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<CartLine, object>>[] propertiesIncluded);

        List<CartLine> FindCartLineBy(System.Linq.Expressions.Expression<Func<CartLine, bool>> predicate);
        Task<List<CartLine>> FindCartLineByAsync(System.Linq.Expressions.Expression<Func<CartLine, bool>> predicate);
    }
}
