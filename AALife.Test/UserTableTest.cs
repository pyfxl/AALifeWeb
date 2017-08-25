using AALife.BLL;
using AALife.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Test
{
    [TestFixture]
    public class UserTableTest
    {
        private UserTableBLL bll;

        [SetUp]
        public void Init()
        {
            bll = new UserTableBLL();
        }


        [Test]
        public void GetUserListTest()
        {
            
        }

        [Test]
        public void GetUserListByDateTest()
        {
            
        }

        [Test]
        public void GetUserListByKeywordsTest()
        {
            
        }

        [Test]
        public void GetUserByUserIdTest()
        {
            UserInfo user = bll.GetUserByUserId(1);
            Assert.AreEqual(user.UserID, 1);
        }

        [Test]
        public void GetUserByUserNameTest()
        {
            UserInfo user = bll.GetUserByUserName("pyfxl");
            Assert.AreEqual(user.UserID, 1);
        }

        [Test]
        public void InsertUserTest()
        {
            UserInfo user = new UserInfo();
            user.UserName = "test1";
            user.UserPassword = "test1";
            bool result = bll.InsertUser(user);
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UpdateUserTest()
        {
            UserInfo user = bll.GetUserByUserId(1);
            user.UserNickName = "冯湘灵";
            bool result = bll.UpdateUser(user);
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UserExistsTest()
        {
            bool result = bll.UserExists("pyfxl");
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UserLoginTest()
        {
            bool result = bll.UserLogin("pyfxl", "pyfxl,");
            Assert.AreEqual(result, true);
        }

        [Test]
        public void DeleteUserTest()
        {
            bool result = bll.DeleteUser(15859);
            Assert.AreEqual(result, true);
        }

    }
}
