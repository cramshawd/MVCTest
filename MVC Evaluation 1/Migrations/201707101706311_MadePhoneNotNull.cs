namespace MVC_Evaluation_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePhoneNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 12));
        }
    }
}
