using System;
using System.Text.RegularExpressions;

namespace WebApi.Services
{
    public interface IPasswordService
    {
        bool ValidatePassword(string password);
        string CreatePassword();
    }

    public class PasswordService : IPasswordService
    {
        public string CreatePassword()
        {
            var upperChar = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            var lowerChar = "abcdefghijkmnopqrstuvwxyz";
            var numbers = "0123456789";
            var specialChar = "!@#_-";

            var password = "";
            var random = new Random();

            while (password.Length < 15)
            {
                password += upperChar[random.Next(0, upperChar.Length)];
                password += lowerChar[random.Next(0, lowerChar.Length)];
                password += numbers[random.Next(0, numbers.Length)];
                password += specialChar[random.Next(0, specialChar.Length)];
            }

            if(!ValidatePassword(password)) CreatePassword();

            return password;
        }

        public bool ValidatePassword(string password)
        {
            var hasMinimumChar = new Regex(@".{15,}");
            var hasUpperChar = new Regex(@"[A-Z]");
            var hasLowerChar = new Regex(@"[a-z]");
            var notHasSequenceCharsRepeat = new Regex(@"(.)\1{1}");
            var hasSpecialChar = new Regex(@"[!@#_\-]");
            
            return (hasMinimumChar.IsMatch(password) &&
                hasUpperChar.IsMatch(password) &&
                hasLowerChar.IsMatch(password) &&
                hasSpecialChar.IsMatch(password)) &&
                (!notHasSequenceCharsRepeat.IsMatch(password));                    
        }
    }
}