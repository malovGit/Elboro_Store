using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Models
{
    public class CartReloadViewModel
    {
        public string Message { get; set; }

        public int TotalCountCart { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPriceCart { get; set; }

        public int CountLineItems { get; set; }

        public int deleteId { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPriceLine { get; set; }
    }
}