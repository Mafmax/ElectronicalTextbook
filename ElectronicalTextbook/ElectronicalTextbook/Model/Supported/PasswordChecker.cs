using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.Supported
{
    public static class PasswordChecker
    {
        
        public static bool IsCorrect(string password, out string message)
        {
            message = "";
            if(!Regex.IsMatch(password,@"[a-zA-Zа-яА-Я0-9]"))
            {
                message = "Символы или цифры";
                return false;
            }
            return true;
        }
        public static string Hashed256(string value)
        {
            var sha = SHA256.Create();
            
            var bytesValue = Encoding.ASCII.GetBytes(value);
            var bytesHash = sha.ComputeHash(bytesValue);
            return BitConverter.ToString(bytesHash).Replace("-","").ToLower();
        }
        public static bool IsCorrectNew(string newPassword, string confirmPassword, out string message)
        {

            bool isCorrect = IsCorrect(newPassword,out var msg);
            bool isEquals = newPassword.Equals(confirmPassword);

            message = "";
            if (!isCorrect)
            {
                message = msg;
            }
            else if (!isEquals)
            {
                message = "Пароли не совпадают";
            }
            return isCorrect && isEquals;
        }
    }
}
