using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service.PayPalService
{
    public class PayPalService : IPayPalService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public PayPalService(IConfiguration configuration)
        {
            _clientId = configuration["Paypal:ClientId"];
            _clientSecret = configuration["Paypal:SecretKey"];
        }

        public Payment CreatePayment(string baseUrl, string intent, string currency, string total, string description)
        {
            var apiContext = GetApiContext();

            var payment = new Payment
            {
                intent = intent,
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
            {
                new Transaction
                {
                    description = description,
                    amount = new Amount
                    {
                        currency = currency,
                        total = total
                    }
                }
            },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = $"{baseUrl}/paypal/cancel",
                    return_url = $"{baseUrl}/paypal/execute"
                }
            };

            return payment.Create(apiContext);
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
        {
            { "mode", "sandbox" }, // Change to "live" in production
            { "clientId", _clientId },
            { "clientSecret", _clientSecret }
        };

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            return new APIContext(accessToken);
        }
    }
}
