using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReciPie.Repositories;

namespace ReciPie.Repositories
{
    public class Repository
    {
        public static async Task<bool> IsPositive(int number) => await Task.FromResult(number > 0);

        public static async Task<bool> IsNegative(int number) => await Task.FromResult(number < 0);

        public static async Task<bool> IsZero(int number) => await Task.FromResult(number == 0);

        public static async Task<bool> ValidarFechaNacimiento(string birthdate)
        {
            bool IsValid = false;
            DateTime date;

            if (DateTime.TryParseExact(birthdate, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }

            return await Task.FromResult(IsValid);
        }

        public static async Task<bool> ValidateEmail(string email)
        {
            bool IsValid = false;

            try
            {
                MailAddress m = new MailAddress(email);
                IsValid = true;
            }
            catch (FormatException)
            {
                IsValid = false;
            }

            return await Task.FromResult(IsValid);
        }

        public static async Task<bool> ValidatePhoneNumber(string phoneNumber)
        {
            bool IsValid = false;

            Regex r = new Regex(@"^([0-9]){10,10}$");
            Match match = r.Match(phoneNumber);

            return await Task.FromResult(IsValid = match.Success ? true : false);
        }

        public static async Task<bool> ValidateText(string text)
        {
            bool IsValid = false;

            foreach (var letter in text.Replace(" ", ""))
            {
                if (!char.IsLetter(letter))
                {
                    IsValid = true;
                }
            }

            return await Task.FromResult(IsValid);
        }

        public static async Task<bool> ValidateInteger(string text)
        {
            bool IsValid = false;

            foreach (var letter in text.Replace(" ", ""))
            {
                if (!char.IsDigit(letter))
                {
                    IsValid = true;
                }
            }

            return await Task.FromResult(IsValid);
        }
    }
}
