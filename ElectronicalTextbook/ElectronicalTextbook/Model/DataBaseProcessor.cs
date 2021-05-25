using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.Model.Supported;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicalTextbook
{
    public static class DataBaseProcessor
    {
        static DataBaseProcessor()
        {
            CheckAdmin();
        }
        private static void CheckAdmin()
        {
            using (var context = new ETContext())
            {
                var admins = context.Admins.ToList();
                if (admins.Count < 1)
                {
                    var admin = new Admin();
                    admin.Username = "admin";
                    admin.PasswordHash = PasswordChecker.Hashed256("admin");
                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
            }
        }
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

        internal static IEnumerable<Speciality> GetSpecialities()
        {
            using (var context = new ETContext())
            {
                foreach (var item in context.Specialities)
                {
                    yield return item;
                }
            }
        }
        internal static bool IsExistMaterial(string theme)
        {
            using (var context = new ETContext())
            {
                var material = context.Materials.Find(theme);
                return material != null;
            }
        }
        internal static IEnumerable<Material> GetOtherMaterials(User user)
        {
            using (var context = new ETContext())
            {
            context.Specialities.Load();
                context.InfoBlocks.Load();
                user = context.FindUserByLogin(user.Username);
                if (user is Teacher teacher)
                {
                    var materials = context.Materials
                        .Where(x => x.Teacher.Id != teacher.Id);
                    foreach (var item in materials)
                    {
                        yield return item;
                    }
                }
                if (user is Student student)
                {
                    foreach (var item in student.CompletedMaterials)
                    {
                        yield return item;
                    }
                }
            }
        }
        internal static IEnumerable<Material> GetInterestingMaterials(User user)
        {
            using (var context = new ETContext())
            {
                context.InfoBlocks.Load();
                context.Specialities.Load();
                user = context.FindUserByLogin(user.Username);
                if (user is Teacher teacher)
                {
                    context.Materials.Load();
                    foreach (var item in teacher.Materials)
                    {
                        yield return item;
                    }
                }
                if (user is Student student)
                {
                    foreach (var item in student.NotCompletedMaterials)
                    {
                        yield return item;
                    }
                }
            }
        }
        internal static IEnumerable<Test> GetOtherTests(User user)
        {
            using (var context = new ETContext())
            {
                user = context.FindUserByLogin(user.Username);
                if (user is Teacher teacher)
                {
                    var tests = context.Tests
                        .Where(x => x.Teacher.Id != teacher.Id);
                    foreach (var item in tests)
                    {
                        yield return item;
                    }
                }
                if (user is Student student)
                {
                    foreach (var item in student.TestEstimations
                        .Where(x => x.Estimation.IsSet))
                    {
                        yield return item.Test;
                    }
                }
            }
        }
        internal static IEnumerable<Test> GetInterestingTests(User user)
        {
            using (var context = new ETContext())
            {
                user = context.FindUserByLogin(user.Username);
                context.Tests.Load();
                if (user is Teacher teacher)
                {
                    foreach (var item in teacher.Tests)
                    {
                        yield return item;
                    }
                }
                if (user is Student student)
                {
                    context.Estimations.Load();
                    foreach (var item in student.TestEstimations.Where(x => !x.Estimation.IsSet))
                    {
                        yield return item.Test;
                    }
                }
            }
        }
        internal static void UploadMaterial(Material material)
        {
            using (var context = new ETContext())
            {
                material.Teacher = context.Teachers
                    .Find(material.Teacher.Id);
                material.Speciality = context.Specialities
                    .Find(material.Speciality.Name);
                for (int i = 0; i < material.StudentsWhoNotComplete.Count; i++)
                {
                    var student = context.Students
                      .Find(material.StudentsWhoNotComplete[i].Id);
                    if (student is null)
                    {
                        student = context.Students.Add(material.StudentsWhoNotComplete[i]);
                    }
                    material.StudentsWhoNotComplete[i] = student;
                }
                for (int i = 0; i < material.StudentsWhoComplete.Count; i++)
                {
                    var student = context.Students
                        .Find(material.StudentsWhoComplete[i].Id);
                    if (student is null)
                    {
                        student = context.Students.Add(material.StudentsWhoComplete[i]);
                    }
                    material.StudentsWhoComplete[i] = student;
                }
                for (int i = 0; i < material.Content.Count; i++)
                {
                    var block = material.Content[i];
                    var findedBlock = context.InfoBlocks.Find(block.Id);
                    material.Content[i] = findedBlock ?? context.InfoBlocks.Add(block);
                }
                var finded = context.Materials
                    .Find(material.Theme);
                var a = material.Content;
                if (finded is null)
                {
                    context.Materials.Add(material);
                }
                else
                {
                    finded = material;
                }

                //   context.Entry(material).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
        internal static IEnumerable<Student> GetStudents(int classNumber, string classChar = "X")
        {
            using (var context = new ETContext())
            {
                var allStudents = context.Students.ToList();
                var students = allStudents.Where(x => x._ClassNumber.Equals(classNumber)).ToList();
                foreach (var student in students)
                {
                    if (classChar.Equals("X"))
                    {
                        yield return student;
                    }
                    else if (student._ClassSymbol.Equals(classChar))
                    {
                        yield return student;
                    }
                }
            }
        }
    }
}
