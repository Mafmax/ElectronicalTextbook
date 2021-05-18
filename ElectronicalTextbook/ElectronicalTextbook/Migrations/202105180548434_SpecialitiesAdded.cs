namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecialitiesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Специальности",
                c => new
                    {
                        Названиеспециальности = c.String(name: "Название специальности", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Названиеспециальности);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Специальности");
        }
    }
}
