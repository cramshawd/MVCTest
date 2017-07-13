namespace MVC_Evaluation_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_EMP_TYPE");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 125),
                        LastName = c.String(nullable: false, maxLength: 25),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        CompanyName = c.String(maxLength: 50),
                        LastLogin = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        EmpType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmpType_Id, cascadeDelete: true)
                .Index(t => t.Email, unique: true, name: "IX_USER_EMAIL")
                .Index(t => t.EmpType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "EmpType_Id", "dbo.EmployeeTypes");
            DropIndex("dbo.Users", new[] { "EmpType_Id" });
            DropIndex("dbo.Users", "IX_USER_EMAIL");
            DropIndex("dbo.EmployeeTypes", "IX_EMP_TYPE");
            DropTable("dbo.Users");
            DropTable("dbo.EmployeeTypes");
        }
    }
}
