using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook.Model.Supported
{
    public static class PasswordChecker
    {
        
        public static bool IsCorrect(string password, out string message)
        {
            throw new NotImplementedException();
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
