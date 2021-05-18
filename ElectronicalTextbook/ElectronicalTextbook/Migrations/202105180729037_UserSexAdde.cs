namespace ElectronicalTextbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSexAdde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Пользователи", "Пол", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Пользователи", "Пол");
        }
    }
}
