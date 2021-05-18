namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHierarchyAdded : DbMigration
    {
        
        public override void Up()
        {
            CreateTable(
                "dbo.Пользователи",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Имяпользователя = c.String(name: "Имя пользователя"),
                        Имя = c.String(),
                        Фамилия = c.String(),
                        Отчество = c.String(),
                        Hashпароля = c.String(name: "Hash пароля"),
                        Speciality_Name = c.String(maxLength: 128),
                        Типпользователя = c.String(name: "Тип пользователя", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Специальности", t => t.Speciality_Name, name:"Специализация")
                .Index(t => t.Speciality_Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Пользователи", "Speciality_Name", "dbo.Специальности");
            DropIndex("dbo.Пользователи", new[] { "Speciality_Name" });
            DropTable("dbo.Пользователи");
        }
    }
}
