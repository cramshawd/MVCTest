namespace MVC_Evaluation_1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MVC_Evaluation_1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Evaluation_1.Models.TestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVC_Evaluation_1.Models.TestContext";
        }

        //protected override void Seed(MVC_Evaluation_1.Models.TestContext context)
        //{
        //    context.EmployeeTypes.AddOrUpdate(e => e.Name,
        //        new EmployeeType() { Name = "Federal - FSA", SortOrder = 1 },
        //        new EmployeeType() { Name = "Federal - CQA", SortOrder = 2 },
        //        new EmployeeType() { Name = "Contractor", SortOrder = 3 }
        //       );
        //    context.Users.AddOrUpdate(u => u.Email,
        //        new User(email: "jdoe@test.biz", lastName: "Doe", firstName: "John", phoneNumber: "", companyName: "", isActive: true, empType: context.EmployeeTypes.Where(et=> et.Name== "Contractor").FirstOrDefault(), createdBy: -1, createdDate: DateTime.UtcNow),
        //        new User(email: "jsmith@test.biz", lastName: "Smith", firstName: "Jane", phoneNumber: "", companyName: "", isActive: true, empType: context.EmployeeTypes.Where(et=> et.Name== "Federal - FSA").FirstOrDefault(), createdBy: -1, createdDate: DateTime.UtcNow)
        //        );
        //}
    }
}
