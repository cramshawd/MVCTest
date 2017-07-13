using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_Evaluation_1;
using MVC_Evaluation_1.Models;
using MVC_Evaluation_1.ViewModels;
using MVC_Evaluation_1.Controllers;

namespace MVC_Evaluation_1.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private Mock<DbSet<User>> GetDbSetMock(IEnumerable<User> items = null)

        {
            if (items == null)
            {
                items = new User[0];
            }
            var dbSetMock = new Mock<DbSet<User>>();
            var q = dbSetMock;

            q.As<IQueryable<User>>().Setup(x => x.Provider).Returns(items.AsQueryable().Provider);

            q.As<IQueryable<User>>().Setup(x => x.Expression).Returns(items.AsQueryable().Expression);

            q.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(items.AsQueryable().ElementType);

            q.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator);
            
            q.Setup(x => x.Find(It.IsAny<User>())).Returns<User[]>(x => items.FirstOrDefault(y => y.Id == x[0].Id));

            q.Setup(x => x.Add(It.IsAny<User>())).Returns<User>(x => x);

            return dbSetMock;

        }
        private Mock<DbSet<EmployeeType>> GetDbSetMock(IEnumerable<EmployeeType> items = null)

        {
            if (items == null)
            {
                items = new EmployeeType[0];
            }
            var dbSetMock = new Mock<DbSet<EmployeeType>>();
            var q = dbSetMock;

            q.As<IQueryable<EmployeeType>>().Setup(x => x.Provider).Returns(items.AsQueryable().Provider);

            q.As<IQueryable<EmployeeType>>().Setup(x => x.Expression).Returns(items.AsQueryable().Expression);

            q.As<IQueryable<EmployeeType>>().Setup(x => x.ElementType).Returns(items.AsQueryable().ElementType);

            q.As<IQueryable<EmployeeType>>().Setup(x => x.GetEnumerator()).Returns(items.GetEnumerator);
            
            q.Setup(m => m.Find(It.IsAny<EmployeeType>())).Returns<EmployeeType[]>(x => items.FirstOrDefault(y => y.Id == x[0].Id));

            q.Setup(x => x.Add(It.IsAny<EmployeeType>())).Returns<EmployeeType>(x => x);

            return dbSetMock;

        }

        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var moq = new Mock<Models.TestContext>();
            var data = new List<User>()
                {
                new User(),
                new User()
                };
            moq.Setup(x => x.Users).Returns(GetDbSetMock(data).Object);
            var controller = new UsersController(moq.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(((List<User>)result.Model).Count == 2);
        }


        [TestMethod]
        public void CreateTest()
        {
            // Arrange

            UsersController controller = new UsersController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePostTest()
        {
            // Arrange
            var moq = new Mock<Models.TestContext>();

            var dataUser = new List<User>()
            {
            };
            var dataEmployeeType = new List<EmployeeType>()
            {
            }.AsEnumerable();
            moq.Setup(x => x.Users).Returns(GetDbSetMock(dataUser).Object);
            moq.Setup(x => x.EmployeeTypes).Returns(GetDbSetMock(dataEmployeeType).Object);
            moq.Setup(x => x.SaveChanges());
            var controller = new UsersController(moq.Object);
            var empType = new EmployeeType(3, "Contractor", 1);
            var user = new User("a@a.a", "a", "a", "123-456-7890", empType, "a", true, -1, new DateTime());
            var userViewModel = new UserViewModel(user);

            // Act
            var result = controller.Create(userViewModel) as ViewResult;

            // Assert
            Assert.IsTrue(((List<User>)result.Model).Count > 0);
        }

        [TestMethod]
        public void EditTest()
        {
            // Arrange
            var moq = new Mock<Models.TestContext>();
            var dataUser = new List<User>()
            {
                new User(4)
            };
            var dataEmployeeType = new List<EmployeeType>()
            {
            };
            moq.Setup(x => x.Users).Returns(GetDbSetMock(dataUser).Object);
            moq.Setup(x => x.EmployeeTypes).Returns(GetDbSetMock(dataEmployeeType).Object);
            moq.Setup(x => x.SaveChanges());
            var controller = new UsersController(moq.Object);
            var empType = new EmployeeType(3, "Contractor", 1);
            var user = new User("a@a.a", "a", "a", "123-456-7890", empType, "a", true, -1, new DateTime());
            var userViewModel = new UserViewModel(user);

            // Act
            ViewResult result = controller.Edit(4, userViewModel) as ViewResult;

            // Assert
            Assert.IsTrue(((List<User>)result.Model).Count > 0);
        }

        [TestMethod]
        public void DeleteWithIdTest()
        {
            // Arrange

            var moq = new Mock<Models.TestContext>();

            var data = new List<User>()
            {
                new User(1),
                new User(4)
            };

            moq.Setup(x => x.Users).Returns(GetDbSetMock(data).Object);

            var controller = new UsersController(moq.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsTrue(((List<User>)result.Model).Count == 1);
        }

        [TestMethod]
        public void DeleteWithNoIdTest()
        {
            // Arrange

            UsersController controller = new UsersController();

            // Act
            ViewResult result = controller.Delete(null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }
    }
}

