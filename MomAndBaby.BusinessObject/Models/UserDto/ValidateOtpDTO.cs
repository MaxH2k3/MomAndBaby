using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.UserDto
{
    public class ValidateOtpDTO
    {
        public string? Email { get; set; }
        public string? Otp { get; set; }

        public ValidateOtpDTO(string? email, string? otp)
        {
            Email = email;
            Otp = otp;
        }
    }
}
