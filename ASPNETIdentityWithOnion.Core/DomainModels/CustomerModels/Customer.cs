using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETIdentityWithOnion.Core.DomainModels.Identity;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System.ComponentModel;

namespace ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels
{
    [Serializable]
    public class Customer : BaseEntity
    {      
        public int? UserId { get; set; }

        public string UserName { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        public string Company { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

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

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string PhoneNumber { get; set; }

        
        [StringLength(24)]
        public string MobileNumber { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        public string AdditionalInfo { get; set; }

        [StringLength(24)]
        public string DateOfBirth { get; set; }

        public string Photo { get; set; }

        public string UpdateDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }



        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
