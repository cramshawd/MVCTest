namespace MVC_Evaluation_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhoneNumber");
        }
    }
}
