using ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels;
using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Extensions
{
    public static class CustomerExtentions
    {
        public static Customer ToCustomer(this CustomerViewModel model)
        {
            Customer customer = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Company = string.IsNullOrWhiteSpace(model.Company) ? "No Company" : model.Company,
                DateOfBirth = model.DateOfBirth,
                MobileNumber = model.MobileNumber,
                PhoneNumber = model.PhoneNumber,
                Fax = model.Fax,
                Address = model.Address,
                City = model.City,
                Region = model.Region,
                Country = model.Country,
                PostalCode = model.PostalCode,
                AdditionalInfo = model.AdditionalInfo,
                Email = model.Email,
                Photo = model.Photo,
                Id = model.CustomerID
                
            };
            return customer;
        }

        public static CustomerViewModel ToCustomerViewModel(this Customer model)
        {
            CustomerViewModel customer = new CustomerViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Company = string.IsNullOrWhiteSpace(model.Company) ? "No Company" : model.Company,
                DateOfBirth = model.DateOfBirth,
                MobileNumber = model.MobileNumber,
                PhoneNumber = model.PhoneNumber,
                Fax = model.Fax,
                Address = model.Address,
                City = model.City,
                Region = model.Region,
                Country = model.Country,
                PostalCode = model.PostalCode,
                AdditionalInfo = model.AdditionalInfo,
                Email = model.Email,
                Photo = model.Photo,
                CustomerID = model.Id,
                
            };
            return customer;
        }
        public static Customer ToCustomer(this CustomerViewModel model,  Customer customer)
        {
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
                customer.Company = string.IsNullOrWhiteSpace(model.Company) ? "No Company" : model.Company;
                customer.DateOfBirth = model.DateOfBirth;
                customer.MobileNumber = model.MobileNumber;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Fax = model.Fax;
                customer.Address = model.Address;
               customer.City = model.City;
               customer.Region = model.Region;
               customer.Country = model.Country;
                customer.PostalCode = model.PostalCode;
                customer.AdditionalInfo = model.AdditionalInfo;
                customer.Email = model.Email;
                customer.Photo = model.Photo;
            return customer;
        }
    }
}