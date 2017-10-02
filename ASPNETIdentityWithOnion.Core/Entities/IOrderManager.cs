using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.Entities
{
    public interface IOrderManager : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<Order> GetOrders();
        Task<IEnumerable<Order>> GetOrdersAsync();

        ApplicationResult UpdateOrder(Order order);
        Task<ApplicationResult> UpdateOrderAsync(Order order);

        Task<ApplicationResult> CreateOrderAsync(Order order);
        ApplicationResult CreateOrder(Order order);

        Task<ApplicationResult> DeleteOrderAsync(int Id);
        ApplicationResult DeleteOrder(int Id);

        Order GetOrderById(int Id);
        Task<Order> GetOrderByIdAsync(int Id);

        Task<ApplicationResult> OrderExistsAsync(int Id);

        Order GetOrderIncluding(int Id, params System.Linq.Expressions.Expression<Func<Order, object>>[] propertiesIncluded);
        Task<Order> GetOrderIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<Order, object>>[] propertiesIncluded);

        List<Order> FindOrderBy(System.Linq.Expressions.Expression<Func<Order, bool>> predicate);
        Task<List<Order>> FindOrderByAsync(System.Linq.Expressions.Expression<Func<Order, bool>> predicate);

        //CartLine
        IEnumerable<OrderDetail> GetOrderDetails();
        Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync();

        ApplicationResult UpdateOrderDetail(OrderDetail orderDetail);
        Task<ApplicationResult> UpdateOrderDetailAsync(OrderDetail orderDetail);

        Task<ApplicationResult> CreateOrderDetailAsync(OrderDetail orderDetail);
        ApplicationResult CreateOrderDetail(OrderDetail orderDetail);

        Task<ApplicationResult> DeleteOrderDetailAsync(int Id);
        ApplicationResult DeleteOrderDetail(int Id);

        OrderDetail GetOrderDetailById(int Id);
        Task<OrderDetail> GetOrderDetailByIdAsync(int Id);

        Task<ApplicationResult> OrderDetailExistsAsync(int Id);

        OrderDetail GetOrderDetailIncluding(int Id, params System.Linq.Expressions.Expression<Func<OrderDetail, object>>[] propertiesIncluded);
        Task<OrderDetail> GetOrderDetailIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<OrderDetail, object>>[] propertiesIncluded);

        List<OrderDetail> FindOrderDetailBy(System.Linq.Expressions.Expression<Func<OrderDetail, bool>> predicate);
        Task<List<OrderDetail>> FindOrderDetailByAsync(System.Linq.Expressions.Expression<Func<OrderDetail, bool>> predicate);
    }
}
