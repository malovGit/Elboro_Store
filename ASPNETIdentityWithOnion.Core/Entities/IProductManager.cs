using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.Entities
{
    public interface IProductManager : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        //paginate
        PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize);
        PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize, Expression<Func<Product, int>> keySelector, OrderBy order);
        PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize, Expression<Func<Product, int>> keySelector, Expression<Func<Product, bool>> predicate, OrderBy order, params Expression<Func<Product, object>>[] includeProperties);

        //SubCategories
        IEnumerable<SubCategory> GetSubCategories();
        Task<IEnumerable<SubCategory>> GetSubCategoriesAsync();

        ApplicationResult UpdateSubCategory(SubCategory subCategory);
        Task<ApplicationResult> UpdateSubCategoryAsync(SubCategory subCategory);

        Task<ApplicationResult> CreateSubCategoryAsync(SubCategory subCategory);
        ApplicationResult CreateSubCategory(SubCategory subCategory);

        Task<ApplicationResult> DeleteSubCategoryAsync(int Id);
        ApplicationResult DeleteSubCategory(int Id);

        SubCategory GetSubCategoryById(int Id);
        Task<SubCategory> GetSubCategoryByIdAsync(int Id);

        Task<ApplicationResult> SubCategoryExistsAsync(int Id);

        //Categories
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<Category>> GetCategoriesAsync();

        ApplicationResult UpdateCategory(Category category);
        Task<ApplicationResult> UpdateCategoryAsync(Category category);

        Task<ApplicationResult> CreateCategoryAsync(Category category);
        ApplicationResult CreateCategory(Category category);

        Task<ApplicationResult> DeleteCategoryAsync(int Id);
        ApplicationResult DeleteCategory(int Id);

        Category GetCategoryById(int Id);
        Task<Category> GetCategoryByIdAsync(int Id);

        Task<ApplicationResult> CategoryExistsAsync(int Id);

        Category FindCategoryByName(string name);

        //Products
        IEnumerable<Product> GetProducts();
        Task<IEnumerable<Product>> GetProductsAsync();

        ApplicationResult UpdateProduct(Product product);
        Task<ApplicationResult> UpdateProductAsync(Product product);
        
        Task<ApplicationResult> CreateProductAsync(Product product);
        ApplicationResult CreateProduct(Product product);
               
        Task<ApplicationResult> DeleteProductAsync(int Id);        
        ApplicationResult DeleteProduct(int Id);

        Product GetProductById(int Id);
        Task<Product> GetProductByIdAsync(int Id);

        Task<ApplicationResult> ProductExistsAsync(int Id);

        //Including
        Category GetCategoryIncluding(int Id, params System.Linq.Expressions.Expression<Func<Category, object>>[] propertiesIncluded);
        Task<Category> GetCategoryIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<Category, object>>[] propertiesIncluded);

        List<Category> FindCategoryBy(System.Linq.Expressions.Expression<Func<Category, bool>> predicate);
        Task<List<Category>> FindCategoryByAsync(System.Linq.Expressions.Expression<Func<Category, bool>> predicate);


        SubCategory GetSubCategoryIncluding(int Id, params System.Linq.Expressions.Expression<Func<SubCategory, object>>[] propertiesIncluded);
        Task<SubCategory> GetSubCategoryIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<SubCategory, object>>[] propertiesIncluded);

        List<SubCategory> FindSubCategoryBy(System.Linq.Expressions.Expression<Func<SubCategory, bool>> predicate);
        Task<List<SubCategory>> FindSubCategoryByAsync(System.Linq.Expressions.Expression<Func<SubCategory, bool>> predicate);


        Product GetProductIncluding(int Id, params System.Linq.Expressions.Expression<Func<Product, object>>[] propertiesIncluded);
        Task<Product> GetProductIncludingAsync(int Id, params System.Linq.Expressions.Expression<Func<Product, object>>[] propertiesIncluded);

        List<Product> FindProductBy(System.Linq.Expressions.Expression<Func<Product, bool>> predicate);
        Task<List<Product>> FindProductByAsync(System.Linq.Expressions.Expression<Func<Product, bool>> predicate);
    }
}
