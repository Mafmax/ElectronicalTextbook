namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InfoBlocksAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InfoBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Material_Theme = c.String(maxLength: 128),
                        Типинформационногоблока = c.String(name: "Тип информационного блока", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Материалы", t => t.Material_Theme)
                .Index(t => t.Material_Theme);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InfoBlocks", "Material_Theme", "dbo.Материалы");
            DropIndex("dbo.InfoBlocks", new[] { "Material_Theme" });
            DropTable("dbo.InfoBlocks");
        }
    }
}
