namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestsAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Пользователи", "Speciality_Name", "dbo.Специальности");
            RenameColumn(table: "dbo.Пользователи", name: "Speciality_Name", newName: "Специализация");
            RenameIndex(table: "dbo.Пользователи", name: "IX_Speciality_Name", newName: "IX_Специализация");
            CreateTable(
                "dbo.Материалы",
                c => new
                    {
                        Темаматериала = c.String(name: "Тема материала", nullable: false, maxLength: 128),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Темаматериала)
                .ForeignKey("dbo.Пользователи", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Тесты",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Темаматериала = c.String(name: "Тема материала", nullable: false, maxLength: 128),
                        Количествопопыток = c.Int(name: "Количество попыток", nullable: false),
                        Student_Id = c.Int(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Материалы", t => t.Темаматериала, cascadeDelete: true)
                .ForeignKey("dbo.Пользователи", t => t.Student_Id)
                .ForeignKey("dbo.Пользователи", t => t.Teacher_Id)
                .Index(t => t.Темаматериала)
                .Index(t => t.Student_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Вопросы",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Номертеста = c.Int(name: "Номер теста", nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Тесты", t => t.Номертеста, cascadeDelete: true)
                .Index(t => t.Номертеста);
            
            CreateTable(
                "dbo.Зависимости материалов",
                c => new
                    {
                        Открываемыйматериал = c.String(name: "Открываемый материал", nullable: false, maxLength: 128),
                        Необходимыйматериалдляоткрытия = c.String(name: "Необходимый материал для открытия", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Открываемыйматериал, t.Необходимыйматериалдляоткрытия })
                .ForeignKey("dbo.Материалы", t => t.Открываемыйматериал)
                .ForeignKey("dbo.Материалы", t => t.Необходимыйматериалдляоткрытия)
                .Index(t => t.Открываемыйматериал)
                .Index(t => t.Необходимыйматериалдляоткрытия);
            
            CreateTable(
                "dbo.МатериалыСпециальности",
                c => new
                    {
                        Темаматериала = c.String(name: "Тема материала", nullable: false, maxLength: 128),
                        Идентификаторматериаланатему = c.String(name: "Идентификатор материала на тему", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Темаматериала, t.Идентификаторматериаланатему })
                .ForeignKey("dbo.Материалы", t => t.Темаматериала, cascadeDelete: true)
                .ForeignKey("dbo.Специальности", t => t.Идентификаторматериаланатему, cascadeDelete: true)
                .Index(t => t.Темаматериала)
                .Index(t => t.Идентификаторматериаланатему);
            
            CreateTable(
                "dbo.Завершенные материалы",
                c => new
                    {
                        Материал = c.String(nullable: false, maxLength: 128),
                        Ученик = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Материал, t.Ученик })
                .ForeignKey("dbo.Материалы", t => t.Материал, cascadeDelete: true)
                .ForeignKey("dbo.Пользователи", t => t.Ученик, cascadeDelete: true)
                .Index(t => t.Материал)
                .Index(t => t.Ученик);
            
            CreateTable(
                "dbo.Незавершенные материалы",
                c => new
                    {
                        Материал = c.String(nullable: false, maxLength: 128),
                        Ученик = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Материал, t.Ученик })
                .ForeignKey("dbo.Материалы", t => t.Материал, cascadeDelete: true)
                .ForeignKey("dbo.Пользователи", t => t.Ученик, cascadeDelete: true)
                .Index(t => t.Материал)
                .Index(t => t.Ученик);
            
            AddForeignKey("dbo.Пользователи", "Специализация", "dbo.Специальности", "Название специальности", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Пользователи", "Специализация", "dbo.Специальности");
            DropForeignKey("dbo.Незавершенные материалы", "Ученик", "dbo.Пользователи");
            DropForeignKey("dbo.Незавершенные материалы", "Материал", "dbo.Материалы");
            DropForeignKey("dbo.Завершенные материалы", "Ученик", "dbo.Пользователи");
            DropForeignKey("dbo.Завершенные материалы", "Материал", "dbo.Материалы");
            DropForeignKey("dbo.МатериалыСпециальности", "Идентификатор материала на тему", "dbo.Специальности");
            DropForeignKey("dbo.МатериалыСпециальности", "Тема материала", "dbo.Материалы");
            DropForeignKey("dbo.Тесты", "Teacher_Id", "dbo.Пользователи");
            DropForeignKey("dbo.Тесты", "Student_Id", "dbo.Пользователи");
            DropForeignKey("dbo.Вопросы", "Номер теста", "dbo.Тесты");
            DropForeignKey("dbo.Тесты", "Тема материала", "dbo.Материалы");
            DropForeignKey("dbo.Материалы", "Teacher_Id", "dbo.Пользователи");
            DropForeignKey("dbo.Зависимости материалов", "Необходимый материал для открытия", "dbo.Материалы");
            DropForeignKey("dbo.Зависимости материалов", "Открываемый материал", "dbo.Материалы");
            DropIndex("dbo.Незавершенные материалы", new[] { "Ученик" });
            DropIndex("dbo.Незавершенные материалы", new[] { "Материал" });
            DropIndex("dbo.Завершенные материалы", new[] { "Ученик" });
            DropIndex("dbo.Завершенные материалы", new[] { "Материал" });
            DropIndex("dbo.МатериалыСпециальности", new[] { "Идентификатор материала на тему" });
            DropIndex("dbo.МатериалыСпециальности", new[] { "Тема материала" });
            DropIndex("dbo.Зависимости материалов", new[] { "Необходимый материал для открытия" });
            DropIndex("dbo.Зависимости материалов", new[] { "Открываемый материал" });
            DropIndex("dbo.Вопросы", new[] { "Номер теста" });
            DropIndex("dbo.Тесты", new[] { "Teacher_Id" });
            DropIndex("dbo.Тесты", new[] { "Student_Id" });
            DropIndex("dbo.Тесты", new[] { "Тема материала" });
            DropIndex("dbo.Материалы", new[] { "Teacher_Id" });
            DropTable("dbo.Незавершенные материалы");
            DropTable("dbo.Завершенные материалы");
            DropTable("dbo.МатериалыСпециальности");
            DropTable("dbo.Зависимости материалов");
            DropTable("dbo.Вопросы");
            DropTable("dbo.Тесты");
            DropTable("dbo.Материалы");
            RenameIndex(table: "dbo.Пользователи", name: "IX_Специализация", newName: "IX_Speciality_Name");
            RenameColumn(table: "dbo.Пользователи", name: "Специализация", newName: "Speciality_Name");
            AddForeignKey("dbo.Пользователи", "Speciality_Name", "dbo.Специальности", "Название специальности");
        }
    }
}
