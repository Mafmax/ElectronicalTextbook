using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Collections;
using System.Collections.Generic;
using ElectronicalTextbook.Model.InfoBlocks;

namespace ElectronicalTextbook.Model.DataBase
{
    public class ETContext : DbContext
    {
        const string local = "data source=(LocalDb)\\MSSQLLocalDB;initial catalog=ElectronicalTextbook.Model.DataBase.Electronical textbook;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        const string sqlServer = "data source=.\\SQLEXPRESS;initial catalog=Electronical textbook;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public ETContext()
            : base(local)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Map<Admin>(x => x.Requires("Тип пользователя").HasValue("Администратор"))
                .Map<Teacher>(x => x.Requires("Тип пользователя").HasValue("Учитель"))
                .Map<Student>(x => x.Requires("Тип пользователя").HasValue("Ученик"));
            modelBuilder.Entity<InfoBlock>()
                  .Map<TextInfoBlock>(x => x.Requires("Тип информационного блока").HasValue("Текст"))
                  .Map<PictureInfoBlock>(x => x.Requires("Тип информационного блока").HasValue("Изображение"));
            modelBuilder.Entity<Question>()
                .HasRequired(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestId);
            modelBuilder.Entity<Test>()
                .HasRequired(x => x.Material)
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.MaterialTheme);
            modelBuilder.Entity<Teacher>()
                .HasRequired(t => t.Speciality)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.SpecialityName);
           /* modelBuilder.Entity<Material>()
                .HasRequired(x => x.Teacher)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.TeacherId);*/
            modelBuilder.Entity<Material>()
                .HasMany(m => m.PreviewMaterials)
                .WithMany(m => m.NextMaterials)
                .Map(pn =>
                {
                    pn.MapLeftKey("Открываемый материал");
                    pn.MapRightKey("Необходимый материал для открытия");
                    pn.ToTable("Зависимости материалов");
                });
            modelBuilder.Entity<Material>()
                .HasMany(m => m.StudentsWhoComplete)
                .WithMany(s => s.CompletedMaterials)
                .Map(ms =>
                {
                    ms.MapLeftKey("Материал");
                    ms.MapRightKey("Ученик");
                    ms.ToTable("Завершенные материалы");
                });
            modelBuilder.Entity<Material>()
                .HasMany(m => m.StudentsWhoNotComplete)
                .WithMany(s => s.NotCompletedMaterials)
                .Map(ms =>
                {
                    ms.MapLeftKey("Материал");
                    ms.MapRightKey("Ученик");
                    ms.ToTable("Незавершенные материалы");
                });


        }
        public IEnumerable<User> GetAllUsers()
        {
            foreach (var item in Admins)
            {
                yield return item;
            }
            foreach (var item in Teachers)
            {
                yield return item;
            }
            foreach (var item in Students)
            {
                yield return item;
            }
        }
        public User FindUserByLogin(string login)
        {
            return GetAllUsers().FirstOrDefault(x => x.Username.Equals(login));
        }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestEstimation> Estimations { get; set; }
        public DbSet<TextInfoBlock> TextBlocks { get; set; }
        public DbSet<PictureInfoBlock> PictureBlocks { get; set; }
        public DbSet<InfoBlock> InfoBlocks { get; set; }
    }

}