
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.Data;
using ASPNETIdentityWithOnion.Core.DomainModels;
using System.Linq.Expressions;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Core.Entities;

namespace ASPNETIdentityWithOnion.Data.Entities
{
    public class ProductManager : IProductManager
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private readonly IRepository<Product> products;
        private readonly IRepository<Category> categories;
        private readonly IRepository<SubCategory> subCategories;
        private bool disposed;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            products = UnitOfWork.Repository<Product>();
            categories = UnitOfWork.Repository<Category>();
            subCategories = UnitOfWork.Repository<SubCategory>();
            
        }

        //FIND
        public Category FindCategoryByName(string name)
        {
            Category res = (from c in categories.GetAll()
                            where c.Name == name
                            select c).First();
            return res;
        }        
        //GetPaginate

        public PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize)
        {
            return products.GetAll(pageIndex, pageSize);
        }
        public PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize, Expression<Func<Product, int>> keySelector, OrderBy order = OrderBy.Ascending)
        {
            return products.GetAll(pageIndex, pageSize, keySelector, order);
        }
        public PaginatedList<Product> GetAllProducts(int pageIndex, int pageSize, Expression<Func<Product, int>> keySelector, Expression<Func<Product, bool>> predicate, OrderBy order = OrderBy.Ascending, params Expression<Func<Product, object>>[] includeProperties)
        {
            return products.GetAll(pageIndex, pageSize, keySelector, predicate, order, includeProperties);
        }
        //GET
        public IEnumerable<SubCategory> GetSubCategories()
        {
            return subCategories.GetAll();
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync()
        {
            return await subCategories.GetAllAsync();
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories.GetAll();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await categories.GetAllAsync();
        }

        public IEnumerable<Product> GetProducts()
        {
            return products.GetAll();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await products.GetAllAsync();
        }

        //UPDATE
        public ApplicationResult UpdateSubCategory(SubCategory subCategory)
        {
            ApplicationResult result;
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategory" }, false);

            }
            else
            {
                subCategories.Update(subCategory);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateSubCategoryAsync(SubCategory subCategory)
        {
            ApplicationResult result;
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategoryAsync" }, false);

            }
            else
            {
                subCategories.Update(subCategory);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateProduct(Product product)
        {
            ApplicationResult result;
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProduct" }, false);

            }
            else
            {
                products.Update(product);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateProductAsync(Product product)
        {
            ApplicationResult result;
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProductAsync" }, false);

            }
            else
            {
                products.Update(product);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult UpdateCategory(Category category)
        {
            ApplicationResult result;
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategory" }, false);

            }
            else
            {
                categories.Update(category);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> UpdateCategoryAsync(Category category)
        {
            ApplicationResult result;
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Error UpdateCategoryAsync" }, false);

            }
            else
            {
                categories.Update(category);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        //CREATE

        public ApplicationResult CreateSubCategory(SubCategory subCategory)
        {
            ApplicationResult result;
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateCategory" }, false);

            }
            else
            {
                subCategories.Insert(subCategory);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateSubCategoryAsync(SubCategory subCategory)
        {
            ApplicationResult result;
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateCategoryAsync" }, false);

            }
            else
            {
                subCategories.Insert(subCategory);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult CreateProduct(Product product)
        {
            ApplicationResult result;
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProduct" }, false);

            }
            else
            {
                products.Insert(product);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CreateProductAsync(Product product)
        {
            ApplicationResult result;
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateProductAsync" }, false);

            }
            else
            {
                products.Insert(product);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult CreateCategory(Category category)
                {
                    ApplicationResult result;
                    if (category == null)
                    {
                        result = new ApplicationResult(new List<string> { "Error CreateCategory" }, false);

                    }
                    else
                    {
                        categories.Insert(category);
                        UnitOfWork.SaveChanges();
                        result = new ApplicationResult(new List<string> { "" }, true);
                    }
                    return result;
                }

        public async Task<ApplicationResult> CreateCategoryAsync(Category category)
        {
            ApplicationResult result;
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Error CreateCategoryAsync" }, false);

            }
            else
            {
                categories.Insert(category);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }



        //DELETE
        public ApplicationResult DeleteSubCategory(int Id)
        {
            ApplicationResult result;
            SubCategory subCategory = subCategories.GetSingle(Id);
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategory" }, false);

            }
            else
            {
                subCategories.Delete(subCategory);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteSubCategoryAsync(int Id)
        {
            ApplicationResult result;
            SubCategory subCategory = await subCategories.GetSingleAsync(Id);
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategoryAsync" }, false);
            }
            else
            {
                subCategories.Delete(subCategory);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteProductAsync(int Id)
        {
            ApplicationResult result;
            Product product = await products.GetSingleAsync(Id);
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteProductAsync" }, false);

            }
            else
            {
                products.Delete(product);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public ApplicationResult DeleteProduct(int Id)
        {
            ApplicationResult result;
            Product product = products.GetSingle(Id);
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteProduct" }, false);

            }
            else
            {
                products.Delete(product);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> DeleteCategoryAsync(int Id)
        {
            ApplicationResult result;
            Category category = await categories.GetSingleAsync(Id);
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategoryAsync" }, false);

            }
            else
            {
                categories.Delete(category);
                await UnitOfWork.SaveChangesAsync();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }        

        public ApplicationResult DeleteCategory(int Id)
        {
            ApplicationResult result;
            Category category = categories.GetSingle(Id);
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Error DeleteCategory" }, false);

            }
            else
            {
                categories.Delete(category);
                UnitOfWork.SaveChanges();
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }



        //GET BY ID
        public SubCategory GetSubCategoryById(int Id)
        {
            return subCategories.GetSingle(Id);
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(int Id)
        {
            return await subCategories.GetSingleAsync(Id);
        }

        public Product GetProductById(int Id)
        {
            return products.GetSingle(Id);
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await products.GetSingleAsync(Id);
        }

        public Category GetCategoryById(int Id)
        {
            return categories.GetSingle(Id);
        }

        public async Task<Category> GetCategoryByIdAsync(int Id)
        {
            return await categories.GetSingleAsync(Id);
        }

        //EXIST
        public async Task<ApplicationResult> SubCategoryExistsAsync(int Id)
        {
            ApplicationResult result;
            SubCategory subCategory = await subCategories.GetSingleAsync(Id);
            if (subCategory == null)
            {
                result = new ApplicationResult(new List<string> { "Category not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> ProductExistsAsync(int Id)
        {
            
            ApplicationResult result;
            Product product = await products.GetSingleAsync(Id);
            if (product == null)
            {
                result = new ApplicationResult(new List<string> { "Product not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        public async Task<ApplicationResult> CategoryExistsAsync(int Id)
        {
            ApplicationResult result;
            Category category = await categories.GetSingleAsync(Id);
            if (category == null)
            {
                result = new ApplicationResult(new List<string> { "Category not found" }, false);

            }
            else
            {
                result = new ApplicationResult(new List<string> { "" }, true);
            }
            return result;
        }

        //Including
        //Category
        public Category GetCategoryIncluding(int Id, params Expression<Func<Category, object>>[] propertiesIncluded)
        {
            return categories.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<Category> GetCategoryIncludingAsync(int Id, params Expression<Func<Category, object>>[] propertiesIncluded)
        {
            return await categories.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<Category> FindCategoryBy(Expression<Func<Category, bool>> predicate)
        {
            var category = categories.FindBy(predicate);
            if (category != null)
            {
                return category;
            }
            return null;
        }
        public async Task<List<Category>> FindCategoryByAsync(Expression<Func<Category, bool>> predicate)
        {
            var category = await categories.FindByAsync(predicate);
            if (category != null)
            {
                return category;
            }
            return null;
        }

        //SubCategory
        public SubCategory GetSubCategoryIncluding(int Id, params Expression<Func<SubCategory, object>>[] propertiesIncluded)
        {
            return subCategories.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<SubCategory> GetSubCategoryIncludingAsync(int Id, params Expression<Func<SubCategory, object>>[] propertiesIncluded)
        {
            return await subCategories.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<SubCategory> FindSubCategoryBy(Expression<Func<SubCategory, bool>> predicate)
        {
            var subcategory = subCategories.FindBy(predicate);
            if (subcategory != null)
            {
                return subcategory;
            }
            return null;
        }
        public async Task<List<SubCategory>> FindSubCategoryByAsync(Expression<Func<SubCategory, bool>> predicate)
        {
            var subcategory = await subCategories.FindByAsync(predicate);
            if (subcategory != null)
            {
                return subcategory;
            }
            return null;
        }

        //Product
        public Product GetProductIncluding(int Id, params Expression<Func<Product, object>>[] propertiesIncluded)
        {
            return products.GetSingleIncluding(Id, propertiesIncluded);
        }
        public async Task<Product> GetProductIncludingAsync(int Id, params Expression<Func<Product, object>>[] propertiesIncluded)
        {
            return await products.GetSingleIncludingAsync(Id, propertiesIncluded);
        }

        public List<Product> FindProductBy(Expression<Func<Product, bool>> predicate)
        {
            var product = products.FindBy(predicate);
            if (product != null)
            {
                return product;
            }
            return null;
        }
        public async Task<List<Product>> FindProductByAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await products.FindByAsync(predicate);
            if (product != null)
            {
                return product;
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
