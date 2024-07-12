using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service.PayPalService
{
    public interface IPayPalService
    {
        Payment CreatePayment(string baseUrl, string intent, string currency, string total, string description);
        Payment ExecutePayment(string paymentId, string payerId);
    }
}
