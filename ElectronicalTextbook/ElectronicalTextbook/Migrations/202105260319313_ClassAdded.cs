namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Пользователи", "Класс", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Пользователи", "Класс");
        }
    }
}
