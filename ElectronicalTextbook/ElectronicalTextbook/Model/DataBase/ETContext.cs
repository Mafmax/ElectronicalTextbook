using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Configuration;
namespace ElectronicalTextbook.Model.DataBase
{
    public class ETContext : DbContext
    {
        const string local = "data source=(LocalDb)\\MSSQLLocalDB;initial catalog=ElectronicalTextbook.Model.DataBase.Electronical textbook;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        const string sqlServer = "data source=.\\SQLEXPRESS;initial catalog=Electronical textbook;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public ETContext()
            : base(sqlServer)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Map<Admin>(x => x.Requires("Тип пользователя").HasValue("Администратор"))
                .Map<Teacher>(x => x.Requires("Тип пользователя").HasValue("Учитель"))
                .Map<Student>(x => x.Requires("Тип пользователя").HasValue("Ученик"));

        }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }

}