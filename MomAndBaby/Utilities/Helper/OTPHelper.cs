using System.Security.Cryptography;

namespace MomAndBaby.Utilities.Helper
{
    public class OTPHelper
    {
        public static string GenerateOTP()
        {
            var randomNumber = new byte[6];
            RandomNumberGenerator.Fill(randomNumber);
            var otp = BitConverter.ToUInt32(randomNumber, 0) % 1000000;
            return otp.ToString("D6");
        }
    }
}
