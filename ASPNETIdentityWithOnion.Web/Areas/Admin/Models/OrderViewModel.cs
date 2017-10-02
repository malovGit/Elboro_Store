using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        //public virtual Customer Customer { get; set; }

        [ScaffoldColumn(false)]
        public string OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string ShippedDate { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }        

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string Region { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }
        //public bool SaveInfo { get; set; }        

        [ScaffoldColumn(false)]
        [Display(Name = "Total Pay")]
        public decimal Total { get; set; }

        [Display(Name = "Descriptions")]
        [StringLength(400)]
        public string Descriptions { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentOptions { get; set; }

        [Display(Name = "Delivery Method")]
        public string DeliveryOptions { get; set; }        

        public int OrderDetails { get; set; }

        public int? CustomerId { get; set; }

        public bool IsPay { get; set; }
    }
}