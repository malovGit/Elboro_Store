using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETIdentityWithOnion.Core.DomainModels
{
    public class EnumClasses
    {
        public enum PaymentOptions
        {
            Visa = 0,
            PayPal,
            Cash,

        };
        public enum DeliveryOptions
        {
            Courier,
            Post,
            Out_In_Stock
        };

    }
}
