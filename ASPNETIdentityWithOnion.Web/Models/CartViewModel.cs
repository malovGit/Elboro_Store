using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class CartViewModel
    {
        //public string Message { get; set; }
        //public decimal CartTotal { get; set; }
        //public int CartCount { get; set; }
        //public int ItemsCount { get; set; }
        //public int DeleteId { get; set; }
        public List<CartLine> Items { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal TotalPayment { get; set; }

        public string GetCurrentPicture(int productId)
        {
            string pic = Items.SingleOrDefault(p => p.Product.Id == productId).Product.Images
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First(); 
                              
            return pic;
        }

        public string ReturnUrl { get; set; }

        public bool SelectedAll { get; set; }


    }
}
			