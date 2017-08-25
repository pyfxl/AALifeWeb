using AALife.BLL;
using AALife.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace AALife.Test
{
    [TestFixture]
    public class ItemTableTest
    {
        private ItemTableBLL bll;

        [SetUp]
        public void Init()
        {
            bll = new ItemTableBLL();
        }


        [Test]
        public void GetItemListByDateTest()
        {
            
        }

    }
}
