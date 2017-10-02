using ASPNETIdentityWithOnion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels;
using System.Linq.Expressions;

namespace ASPNETIdentityWithOnion.Data.Entities
{
    public class CustomersManager : ICustomersManager
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private IRepository<Customer> customers;
        private bool disposed;

        public CustomersManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            customers = UnitOfWork.Repository<Customer>();
        }


        public IEnumerable<Customer> GetCustomers()
        {
            return customers.GetAll();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await customers.GetAllAsync();
        }

        public ApplicationResult UpdateCustomer(Customer customer)
        {
            ApplicationResult result;
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategory" }, false);

            }
            else
            {
                customers.Update(customer);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateCustomerAsync(Customer customer)
        {
            ApplicationResult result;
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategoryAsync" }, false);

            }
            else
            {
                customers.Update(customer);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult CreateCustomer(Customer customer)
        {
            ApplicationResult result;
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProduct" }, false);

            }
            else
            {
                customers.Insert(customer);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateCustomerAsync(Customer customer)
        {
            ApplicationResult result;
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProductAsync" }, false);

            }
            else
            {
                customers.Insert(customer);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }
       
        public ApplicationResult DeleteCustomer(int Id)
        {

            ApplicationResult result;
            Customer customer = customers.GetSingle(Id);
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategory" }, false);

            }
            else
            {
                customers.Delete(customer);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteCustomerAsync(int Id)
        {
            ApplicationResult result;
            Customer customer = await customers.GetSingleAsync(Id);
            if (customer == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategoryAsync" }, false);
            }
            else
            {
                customers.Delete(customer);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public Customer GetCustomerById(int Id)
        {
            return customers.GetSingle(Id);
        }

        public async Task<Customer> GetCustomerByIdAsync(int Id)
        {
            return await customers.GetSingleAsync(Id);
        }

        public async Task<ApplicationResult> CustomerExistsAsync(int Id)
        {
            ApplicationResult result;
            if (await customers.FindByAsync(e=> e.Id == Id) == null)
            {
                result = new ApplicationResult(new List<string> { "Category not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public Customer GetCustomerIncluding(int Id, params Expression<Func<Customer, object>>[] propertiesIncluded)
        {
            return customers.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<Customer> GetCustomerIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<Customer, object>>[] propertiesIncluded)
        {
            return await customers.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<Customer> FindCustomerBy(Expression<Func<Customer, bool>> predicate)
        {
            var cust = customers.FindBy(predicate);
            if (cust != null)
            {
                return cust;
            }
            return null;
        }
        public async Task<List<Customer>> FindCustomerByAsync(Expression<Func<Customer, bool>> predicate)
        {
            var cust = await customers.FindByAsync(predicate);
            if(cust != null)
            {
                return cust;
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
