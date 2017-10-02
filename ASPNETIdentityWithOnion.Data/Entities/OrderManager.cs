using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Data.Entities
{
    public class OrderManager : IOrderManager
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private IRepository<Order> orders;
        private IRepository<OrderDetail> details;
        private bool disposed;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            orders = UnitOfWork.Repository<Order>();
            details = UnitOfWork.Repository<OrderDetail>();
        }


        //Orders
        public ApplicationResult CreateOrder(Order order)
        {
            ApplicationResult result;
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProduct" }, false);

            }
            else
            {
                orders.Insert(order);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateOrderAsync(Order order)
        {
            ApplicationResult result;
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProductAsync" }, false);

            }
            else
            {
                orders.Insert(order);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult DeleteOrder(int Id)
        {
            ApplicationResult result;
            Order order = orders.GetSingle(Id);
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategory" }, false);

            }
            else
            {
                orders.Delete(order);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteOrderAsync(int Id)
        {
            ApplicationResult result;
            Order order = await orders.GetSingleAsync(Id);
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategoryAsync" }, false);
            }
            else
            {
                orders.Delete(order);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateOrder(Order order)
        {
            ApplicationResult result;
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategory" }, false);

            }
            else
            {
                orders.Update(order);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateOrderAsync(Order order)
        {
            ApplicationResult result;
            if (order == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategoryAsync" }, false);

            }
            else
            {
                orders.Update(order);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders.GetAll();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await orders.GetAllAsync();
        }

        public Order GetOrderById(int Id)
        {
            return orders.GetSingle(Id);
        }

        public async Task<Order> GetOrderByIdAsync(int Id)
        {
            return await orders.GetSingleAsync(Id);
        }

        public async Task<ApplicationResult> OrderExistsAsync(int Id)
        {
            ApplicationResult result;
            if (await orders.FindByAsync(e => e.Id == Id) == null)
            {
                result = new ApplicationResult(new List<string> { "Order not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public Order GetOrderIncluding(int Id, params Expression<Func<Order, object>>[] propertiesIncluded)
        {
            return orders.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<Order> GetOrderIncludingAsync(int Id, params Expression<Func<Order, object>>[] propertiesIncluded)
        {
            return await orders.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<Order> FindOrderBy(Expression<Func<Order, bool>> predicate)
        {
            var cart = orders.FindBy(predicate);
            if (cart != null)
            {
                return cart;
            }
            return null;
        }
        public async Task<List<Order>> FindOrderByAsync(Expression<Func<Order, bool>> predicate)
        {
            var cart = await orders.FindByAsync(predicate);
            if (cart != null)
            {
                return cart;
            }
            return null;
        }

        //OrderDetail
        public ApplicationResult CreateOrderDetail(OrderDetail orderDetail)
        {
            ApplicationResult result;
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateLine" }, false);

            }
            else
            {
                details.Insert(orderDetail);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            ApplicationResult result;
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateLineAsync" }, false);

            }
            else
            {
                details.Insert(orderDetail);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult DeleteOrderDetail(int Id)
        {
            ApplicationResult result;
            OrderDetail orderDetail = details.GetSingle(Id);
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteLine" }, false);

            }
            else
            {
                details.Delete(orderDetail);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteOrderDetailAsync(int Id)
        {
            ApplicationResult result;
            OrderDetail orderDetail = await details.GetSingleAsync(Id);
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteLineAsync" }, false);
            }
            else
            {
                details.Delete(orderDetail);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateOrderDetail(OrderDetail orderDetail)
        {
            ApplicationResult result;
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateLine" }, false);

            }
            else
            {
                details.Update(orderDetail);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            ApplicationResult result;
            if (orderDetail == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateLineAsync" }, false);

            }
            else
            {
                details.Update(orderDetail);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return details.GetAll();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync()
        {
            return await details.GetAllAsync();
        }

        public OrderDetail GetOrderDetailById(int Id)
        {
            return details.GetSingle(Id);
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int Id)
        {
            return await details.GetSingleAsync(Id);
        }

        public async Task<ApplicationResult> OrderDetailExistsAsync(int Id)
        {
            ApplicationResult result;
            if (await details.FindByAsync(e => e.Id == Id) == null)
            {
                result = new ApplicationResult(new List<string> { "OrderDetail not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public OrderDetail GetOrderDetailIncluding(int Id, params Expression<Func<OrderDetail, object>>[] propertiesIncluded)
        {
            return details.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<OrderDetail> GetOrderDetailIncludingAsync(int Id, params Expression<Func<OrderDetail, object>>[] propertiesIncluded)
        {
            return await details.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<OrderDetail> FindOrderDetailBy(Expression<Func<OrderDetail, bool>> predicate)
        {
            var orderDetail = details.FindBy(predicate);
            if (orderDetail != null)
            {
                return orderDetail;
            }
            return null;
        }
        public async Task<List<OrderDetail>> FindOrderDetailByAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            var orderDetail = await details.FindByAsync(predicate);
            if (orderDetail != null)
            {
                return orderDetail;
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
