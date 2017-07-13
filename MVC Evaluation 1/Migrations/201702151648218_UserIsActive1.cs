namespace MVC_Evaluation_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIsActive1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsActive");
        }
    }
}
