using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Authy.Net;
using Twilio;
using Twilio.Clients;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;


namespace ASPNETIdentityWithOnion.Data.Identity
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            string AccountSid = "AC6e489f9839e4c7a41e5bec234633e044";
            string AuthToken = "db096c5ca14ac9516edbf16c4df88248";
            //string TwilioPhoneNumber = "+1204-819-0363";

            //var twilio = new TwilioRestClient(AccountSid, AuthToken);
            TwilioClient.Init(AccountSid, AuthToken);

            var messageToSend = MessageResource.CreateAsync(from: new PhoneNumber("+1204-819-0363"),
                                                                to: new PhoneNumber(message.Destination),
                                                                body: message.Body);            
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
