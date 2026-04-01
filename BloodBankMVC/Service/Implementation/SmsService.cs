using BloodBankMVC.Models;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BloodBankMVC.Service.Implementation
{
    public class SmsService
    {
        private readonly TwilioSettings _settings;

        public SmsService(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendSmsAsync(string mobileNumber, string message)
        {
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);

            var formattedNumber = mobileNumber.StartsWith("+")
                ? mobileNumber
                : "+977" + mobileNumber;

            await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_settings.FromNumber),
                to: new Twilio.Types.PhoneNumber(formattedNumber)
            );
        }
    }
}
