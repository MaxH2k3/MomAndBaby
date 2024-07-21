using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Helper
{
    public class PasswordHelper
    {
        private static readonly Random Random = new Random();
        private const string UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string SpecialChars = "!@#$%^&*";


        public static string GeneratePassword(int length = 6)
        {
            if (length < 6)
                throw new ArgumentException("Password length must be at least 6 characters.");

            // Ensure the password has at least one upper letter, one number, and one special character
            var password = new StringBuilder();
            password.Append(UpperLetters[Random.Next(UpperLetters.Length)]);
            password.Append(LowerLetters[Random.Next(LowerLetters.Length)]);
            password.Append(Numbers[Random.Next(Numbers.Length)]);
            password.Append(SpecialChars[Random.Next(SpecialChars.Length)]);

            // Fill the remaining characters randomly
            string allChars = UpperLetters + LowerLetters + Numbers + SpecialChars;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[Random.Next(allChars.Length)]);
            }

            // Shuffle the password to ensure randomness
            return new string(password.ToString().ToCharArray().OrderBy(s => (Random.Next(2) % 2) == 0).ToArray());
        }
    }
}
