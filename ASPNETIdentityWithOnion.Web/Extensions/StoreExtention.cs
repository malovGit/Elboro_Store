using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using ASPNETIdentityWithOnion.Web.Models;
using ASPNETIdentityWithOnion.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ASPNETIdentityWithOnion.Web.Extensions
{
    public static class StoreExtention
    {
        public static Product ToProduct(this Areas.Admin.Models.ProductViewModel pm)
        {
            if(pm == null)
            {
                return null;
            }
            var product = new Product()
            {
                Id = pm.ProductID,
                Name = pm.Name,
                IsActive = pm.IsActive,
                Description = pm.ShortDescription,
                Price = pm.Price,
                LongDescription = pm.LongDescription,
                SubCategoryId = pm.SubSelectedId,
                Images = pm.Path                 
                             
            };
            //if(pm.Path != null)
            //{
            //    char[] delim = { ',' };
            //    string[] res = pm.Path.Split(delim,StringSplitOptions.RemoveEmptyEntries);
            //    product.Images = new List<string>();
            //    product.Images.AddRange(res);
            //}
            //if(pm.Images != null)
            //{
            //    product.Images = pm.Images;
            //}
            return product;
        }

        public static ProductViewModel ToProductViewModel(this Product prod)
        {
            if(prod == null)
            {
                return null;
            }
            Areas.Admin.Models.ProductViewModel product = new Areas.Admin.Models.ProductViewModel()
            {
                ProductID = prod.Id,
                Name = prod.Name,
                ShortDescription = prod.Description,
                LongDescription = prod.LongDescription,
                IsActive = prod.IsActive,
                Price = prod.Price,
                //SubCategoryName = prod.SubCategory.Name,
                SubSelectedId = prod.SubCategoryId.Value,
                Path = prod.Images                    
            };
            if(product.Path != null)
            {
                
                char[] delim = { ',' };
                string[] res = product.Path.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                product.Images = new List<string>(res.ToList());
            }
                
            return product;
        }

        public static Category ToCategory(this CategoryViewModel cvm)
        {
            if(cvm == null)
            {
                return null;
            }
            var category = new Category()
            {
                Id = cvm.CategoryId,
                Name = cvm.Name,
                IsActive = cvm.IsActive,
                Path = cvm.Path,
                SubCategories = cvm.SubCategories
            };
            return category;
        }

        public static CategoryViewModel ToCategoryViewModel(this Category cat)
        {
            if(cat == null)
            {
                return null;
            }
            CategoryViewModel category = new CategoryViewModel()
            {
                CategoryId = cat.Id,
                Name = cat.Name,
                IsActive = cat.IsActive,
                Path = cat.Path,
                SubCategories = cat.SubCategories

            };
            return category;
        }

        public static SubCategory ToSubCategory(this Areas.Admin.Models.SubCategoryViewModel scvm)
        {
            if (scvm == null)
            {
                return null;
            }
            var subCategory = new SubCategory()
            {
                Id = scvm.SubCategoryId,
                Name = scvm.Name,
                IsActive = scvm.IsActive,
                Path = scvm.Path,
                CategoryId = scvm.SelectedId,
                Products = scvm.Products
            };
            return subCategory;
        }

        public static Areas.Admin.Models.SubCategoryViewModel ToSubCategoryViewModel(this SubCategory sc)
        {
            if (sc == null)
            {
                return null;
            }
            Areas.Admin.Models.SubCategoryViewModel subCategory = new Areas.Admin.Models.SubCategoryViewModel()
            {
                SubCategoryId = sc.Id,
                Name = sc.Name,
                IsActive = sc.IsActive,
                Path = sc.Path,
                SelectedId = sc.CategoryId.Value,
                Products = sc.Products,
            };
            return subCategory;
        }

        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            if(order == null)
            {
                return null;
            }
            OrderViewModel orderVM = new OrderViewModel()
            {
                OrderId = order.Id,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                OrderDate = order.OrderDate.ToLongDateString(),
                ShippedDate = order.ShippedDate.ToLongDateString(),
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                Region = order.Region,
                PostalCode = order.PostalCode,
                Phone = order.Phone,
                Fax = order.Fax,
                DeliveryOptions = order.DeliveryOptions.ToString(),
                PaymentOptions = order.PaymentOptions.ToString(),
                Total = order.Total,
                Descriptions = order.Descriptions,
                OrderDetails = order.OrderDetails.Count,
                CustomerId = order.CustomerId,
                UserName = order.UserName,
                IsPay = order.PaidOrder                                        
            };
            return orderVM;
        }

        public static Order ToOrder(this OrderViewModel model)
        {
            if(model == null)
            {
                return null;
            }
            Order order = new Order()
            {
                Id = model.OrderId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                Region = model.Region,
                PostalCode = model.PostalCode,
                Phone = model.Phone,
                Descriptions = model.Descriptions,
                Fax = model.Fax,
                CustomerId = model.CustomerId,
                DeliveryOptions = (EnumClasses.DeliveryOptions)Enum.Parse(typeof(EnumClasses.DeliveryOptions), model.DeliveryOptions),
                PaymentOptions = (EnumClasses.PaymentOptions)Enum.Parse(typeof(EnumClasses.PaymentOptions), model.PaymentOptions),
                OrderDate = DateTime.Parse(model.OrderDate),
                SaveInfo = true,
                ShippedDate = DateTime.Parse(model.ShippedDate),
                Total = model.Total,
                UserName = model.UserName,
                PaidOrder = model.IsPay,
                Experation = DateTime.Now,
                CcType = "000000000",
                CreditCard = "MasterCard"
            };
            return order;
        }

        public static Order FromEditOrder(this OrderEditViewModel model, Order order)
        {
            if (model == null || order == null)
            {
                return null;
            }
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Address = model.Address;
            order.City = model.City;
            order.Country = model.Country;
            order.Region = model.Region;
            order.PostalCode = model.PostalCode;
            order.Phone = model.Phone;
            order.Descriptions = model.Descriptions;
            order.Fax = model.Fax;
            order.PaidOrder = model.IsPay;
            return order;
        }

        public static OrderEditViewModel ToOrderEditViewModel(this Order order)
        {
            if (order == null)
            {
                return null;
            }
            OrderEditViewModel orderVM = new OrderEditViewModel()
            {
                OrderId = order.Id,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                Region = order.Region,
                PostalCode = order.PostalCode,
                Phone = order.Phone,
                Fax = order.Fax,
                DeliveryOptions = order.DeliveryOptions.ToString(),
                PaymentOptions = order.PaymentOptions.ToString(),
                Descriptions = order.Descriptions,
                IsPay = order.PaidOrder
            };
            return orderVM;
        }

        //public static IEnumerable<SelectListItem> ToDropDownListCategory( IEnumerable<Category> list)
        //{
        //    IEnumerable<SelectListItem> selectList = from item in list
        //                                             select new SelectListItem
        //                                             {
        //                                                 Text = item.Name,
        //                                                 Value = item.Id.ToString(),
        //                                                 Selected = false
        //                                             };
        //    return selectList;
        //}
    }
}