using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.MessageConstant
{
    public class MessageOtp
    {
        public const string OTPSent = "OTP sent successfully";
        public const string OTPUnvalid = "OTP unvalid";
        public const string ResendOTP = "OTP was sent again in your email";
        public const string OTPStillValid = "The OTP code sent in your email is valid for 5 minutes from the time the email is sent";
    }
}
