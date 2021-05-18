using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.Model.Supported;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook
{
    public static class DataBaseProcessor
    {

        public static bool IsUsernameAvaliable(string username)
        {
            using (var context = new ETContext())
            {
                var user = context.FindUserByLogin(username);
                return user is null;
            }
        }
        public static bool TryEnter(string username, string password, out User user)
        {
            user = null;
            using (var context = new ETContext())
            {
                var findedUser = context.FindUserByLogin(username);
                if (findedUser is null) return false;
                string hash = PasswordChecker.Hashed256(password);
                if (!findedUser.PasswordHash.Equals(hash)) return false;
                else { user = findedUser; return true; }
            }
        }

        internal static IEnumerable<string> GetUserNames()
        {
            using (var context = new ETContext())
            {
                var users = context.GetAllUsers()
                    .Select(x => x.Username)
                    .Where(x => !x.Equals("admin"));

                foreach (var item in users)
                {
                    yield return item;
                }
            }
        }

        public static bool IsCorrectPassword(string username, string password)
        {
            if (!PasswordChecker.IsCorrect(username, out var plug))
            {
                return false;
            }
            using (var context = new ETContext())
            {
                User user = context.FindUserByLogin(username);
                if (user is null) return false;
                string hash = PasswordChecker.Hashed256(password);
                return user.PasswordHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
            }
        }
        internal static void AddSpeciality(string speciality)
        {
            using (var context = new ETContext())
            {
                var s = context.Specialities.FirstOrDefault(x => x.Name
                .Equals(speciality, StringComparison.OrdinalIgnoreCase));
                if (s is null)
                {
                    s = new Speciality() { Name = speciality };
                    context.Specialities.Add(s);
                    context.SaveChanges();
                }
            }
        }
        internal static void RegisterStudent(Student student,
            string classNumber,
            string classSymbol)
        {
            student._ClassNumber = int.Parse(classNumber);
            student._ClassSymbol = classSymbol;
            using (var context = new ETContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }
        internal static void RegisterTeacher(Teacher teacher, string specialization)
        {
            using (var context = new ETContext())
            {
                var speciality = context.Specialities.First(x => x.Name.Equals(specialization));
                teacher.Speciality = speciality;
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }
        internal static void ChangePassword(string username, string password)
        {
            using (var context = new ETContext())
            {
                var user = context.FindUserByLogin(username);
                user.PasswordHash = PasswordChecker.Hashed256(password);
                context.SaveChanges();
            }
        }

        internal static string[] GetSpecialities()
        {
            using (var context = new ETContext())
            {
                return context.Specialities.Select(x => x.Name).ToArray();
            }
        }
    }
}
