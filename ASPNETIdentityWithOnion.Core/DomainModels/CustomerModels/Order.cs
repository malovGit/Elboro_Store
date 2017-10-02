using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASPNETIdentityWithOnion.Core.DomainModels.EnumClasses;

namespace ASPNETIdentityWithOnion.Core.DomainModels.CustomerModels
{
    public class Order : BaseEntity
    {
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime ShippedDate { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

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

        public bool SaveInfo { get; set; }

        public bool PaidOrder { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [Display(Name = "Payment Options")]
        public PaymentOptions PaymentOptions { get; set; }

        [Display(Name = "Descriptions")]
        [StringLength(400)]
        public string Descriptions { get; set; }

        [Display(Name = "Delivery Options")]       
        public DeliveryOptions DeliveryOptions { get; set; }

        
        [StringLength(24)]
        public string Fax { get; set; }

        //payment properties
        [Display(Name = "Experation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Experation { get; set; }

        [Display(Name = "Credit Card")]
        //[NotMapped]
        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }

        [Display(Name = "Credit Card Type")]
        //[NotMapped]
        public string CcType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }


        public string ToString(Order order)
        {
            StringBuilder strOrder = new StringBuilder();

            strOrder.Append("<p>Order Information for Order: " + order.Id + "<br>Placed at: " + order.OrderDate + "</p>").AppendLine();
            strOrder.Append("<p>Name: " + order.FirstName + " " + order.LastName + "<br>");
            strOrder.Append("Address: " + order.Address + " " + order.City + " " + order.Region + " " + order.PostalCode + "<br>");
            strOrder.Append("Contact: " + order.Email + " " + order.Phone + "</p>" + "<br>" + "<p>Delivery Options:" + order.DeliveryOptions + "Payment Options:" + order.PaymentOptions + "</p>");
            //strOrder.Append("<p>Charge: " + order.CreditCard + " " + order.Experation.ToString("dd-MM-yyyy") + "</p>");
            //strOrder.Append("<p>Credit Card Type: " + order.CcType + "</p>");

            strOrder.Append("<br>").AppendLine();
            strOrder.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            strOrder.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    strOrder.Append("<tr>");
                    output = "<td>" + item.Product.Name + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    strOrder.Append(output).AppendLine();
                    Console.WriteLine(output);
                    strOrder.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered." + ex.StackTrace.ToString();
            }
            strOrder.Append("</Table>");
            strOrder.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", order.Total);
            strOrder.Append(footer).AppendLine();
            strOrder.Append("</b>");

            return strOrder.ToString();
        }
    }
}
