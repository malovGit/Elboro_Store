using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ASPNETIdentityWithOnion.Core.Entities
{
    public interface ICustomersManager : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<Customer> GetCustomers();
        Task<IEnumerable<Customer>> GetCustomersAsync();

        ApplicationResult UpdateCustomer(Customer customer);
        Task<ApplicationResult> UpdateCustomerAsync(Customer customer);

        Task<ApplicationResult> CreateCustomerAsync(Customer customer);
        ApplicationResult CreateCustomer(Customer customer);

        Task<ApplicationResult> DeleteCustomerAsync(int Id);
        ApplicationResult DeleteCustomer(int Id);

        Customer GetCustomerById(int Id);
        Task<Customer> GetCustomerByIdAsync(int Id);

        Task<ApplicationResult> CustomerExistsAsync(int Id);

        Customer GetCustomerIncluding(int Id, params Expression<Func<Customer, object>>[] propertiesIncluded);

        Task<Customer> GetCustomerIncludingAsync(int Id, params Expression<Func<Customer, object>>[] propertiesIncluded);


        List<Customer> FindCustomerBy(Expression<Func<Customer, bool>> predicate);

        Task<List<Customer>> FindCustomerByAsync(Expression<Func<Customer, bool>> predicate);
       
    }
}