namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimationAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Тесты", "Student_Id", "dbo.Пользователи");
            DropIndex("dbo.Тесты", new[] { "Student_Id" });
            CreateTable(
                "dbo.Оценки за тесты",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Количествопопыток = c.Int(name: "Количество попыток", nullable: false),
                        Оценка = c.String(),
                        Test_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Тесты", t => t.Test_Id)
                .ForeignKey("dbo.Пользователи", t => t.Student_Id)
                .Index(t => t.Test_Id)
                .Index(t => t.Student_Id);
            
            DropColumn("dbo.Тесты", "Количество попыток");
            DropColumn("dbo.Тесты", "Student_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Тесты", "Student_Id", c => c.Int());
            AddColumn("dbo.Тесты", "Количество попыток", c => c.Int(nullable: false));
            DropForeignKey("dbo.Оценки за тесты", "Student_Id", "dbo.Пользователи");
            DropForeignKey("dbo.Оценки за тесты", "Test_Id", "dbo.Тесты");
            DropIndex("dbo.Оценки за тесты", new[] { "Student_Id" });
            DropIndex("dbo.Оценки за тесты", new[] { "Test_Id" });
            DropTable("dbo.Оценки за тесты");
            CreateIndex("dbo.Тесты", "Student_Id");
            AddForeignKey("dbo.Тесты", "Student_Id", "dbo.Пользователи", "Id");
        }
    }
}
