using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.UserDto
{
    public class UpdateUserDto
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Otp { get; set; }

        public UpdateUserDto(string? userName, string? fullName, string? password, string? phoneNumber, string? address, string? otp)
        {
            UserName = userName;
            FullName = fullName;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            Otp = otp;
        }
    }
}
