using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AutenticacionASP.Models.ServiceMessage
{
    public class TwilioHelper
    {
        private readonly IConfiguration _configuration;
        public TwilioHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SendSMSMessage(string toMobilePhone, string messageToSend)
        {
            var _twilio_Account_SID = _configuration["Twilio_Account_SID"];
            var _twilio_Auth_TOKEN = _configuration["Twilio_Auth_TOKEN"];
            var _twilio_Phone_Number = _configuration["Twilio_Phone_Number"];

            TwilioClient.Init(
                        _twilio_Account_SID,
                        _twilio_Auth_TOKEN
                    );

            if (messageToSend.Length > 6)
                messageToSend = messageToSend.Substring(0, 6);

            var message = MessageResource.Create(
                from: new PhoneNumber(_twilio_Phone_Number),
                to: new PhoneNumber("+58" + toMobilePhone),
                body: messageToSend
                );

            return message.Sid;
        }
    }
}
