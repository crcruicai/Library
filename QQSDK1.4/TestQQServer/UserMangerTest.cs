using QQServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QQMessage;
using System.Collections.Generic;

namespace TestQQServer
{


    [TestClass()]
    public class UserMangerTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        private void AddUser()
        {
            


        }

   






 




        [TestMethod()]
        public void CloseUserTest()
        {
            UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                target.CloseUser("Administrator").IsTrue();         
            }
            else
            {
                Assert.Fail("Login Fail");
            }
        }



        [TestMethod()]
        public void LoginTest()
        {
             UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Login Fail");
            }
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                
                User user = new User("Liming", "123456");
                
                target.CreateUser("Administrator", user).IsTrue();
              
            }
            else
            {
                Assert.Fail("CreateUser Fail");
            }
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {

                User user = new User("Liming222", "123456");
                target.CreateUser("Administrator", user);
                target.DeleteUser("Administrator", user).IsTrue ();

            }
            else
            {
                Assert.Fail("CreateUser Fail");
            }
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                User user = new User("Liming2", "123456");
                target.CreateUser("Administrator", user).IsTrue ();
                user.Password = "152463";
                target.UpdateUser("Administrator", user).IsTrue ();
                
            }
            else
            {
                Assert.Fail("UpdateUser Fail");
            }
        }

        [TestMethod()]
        public void GetUserListTest()
        {
           UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                List<User> list = target.GetUserList("Administrator");
                list.IsNull();
            }
            else
            {
                Assert.Fail("GetUserList Fail");
            }
        }


        [TestMethod()]
        public void KeepOnlineTest()
        {
            UserManger target = new UserManger();
            LoginTest();
            target.KeepOnline("Administrator");
            target.GetUserList("Administrator").IsNull();
        }


        [TestMethod]
        public void GetUserTest()
        {
             UserManger target = new UserManger();
            User login = new User("Administrator", "Administrator");
            if (target.Login(login) == LoginResult.Succeed)
            {
                User user = target.GetUser("Administrator");
                user.Name.AreEqualWith(login.Name);

                if (user.Type == User.UserType.Admin)
                {
                    Assert.IsTrue(true);
                }
                
            }
        }



 



    }
}
