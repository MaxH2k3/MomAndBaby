using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Helper
{
    public class UserHelper
    {
        public static string GetProvince(string input)
        {
            string[] parts = input.Split(',');

            if (parts.Length < 3)
            {
                return string.Empty; // Not enough parts to have two commas
            }

            return parts[1].Trim(); // Return the part between the first and second comma
        }

        public static string GetUsernameFromEmail(string? email)
        {
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Invalid email address", nameof(email));
            }

            return email.Split('@')[0];
        }

    }
}
