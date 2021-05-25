namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaterialHasSingleSpecialityNow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.МатериалыСпециальности", "Тема материала", "dbo.Материалы");
            DropForeignKey("dbo.МатериалыСпециальности", "Идентификатор материала на тему", "dbo.Специальности");
            DropIndex("dbo.МатериалыСпециальности", new[] { "Тема материала" });
            DropIndex("dbo.МатериалыСпециальности", new[] { "Идентификатор материала на тему" });
            AddColumn("dbo.Материалы", "Speciality_Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Материалы", "Speciality_Name");
            AddForeignKey("dbo.Материалы", "Speciality_Name", "dbo.Специальности", "Название специальности");
            DropTable("dbo.МатериалыСпециальности");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.МатериалыСпециальности",
                c => new
                    {
                        Темаматериала = c.String(name: "Тема материала", nullable: false, maxLength: 128),
                        Идентификаторматериаланатему = c.String(name: "Идентификатор материала на тему", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Темаматериала, t.Идентификаторматериаланатему });
            
            DropForeignKey("dbo.Материалы", "Speciality_Name", "dbo.Специальности");
            DropIndex("dbo.Материалы", new[] { "Speciality_Name" });
            DropColumn("dbo.Материалы", "Speciality_Name");
            CreateIndex("dbo.МатериалыСпециальности", "Идентификатор материала на тему");
            CreateIndex("dbo.МатериалыСпециальности", "Тема материала");
            AddForeignKey("dbo.МатериалыСпециальности", "Идентификатор материала на тему", "dbo.Специальности", "Название специальности", cascadeDelete: true);
            AddForeignKey("dbo.МатериалыСпециальности", "Тема материала", "dbo.Материалы", "Тема материала", cascadeDelete: true);
        }
    }
}
