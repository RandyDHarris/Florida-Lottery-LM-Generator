//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        GetLuckyNumbersFromMockData
//Description:  This is a test class that uses mock data to execute 
//              test that test the Florida Lottery Lucky Money retrieval.

#region Assemblies
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using Business;
using Test.Mock;
#endregion

namespace Test
{
    [TestClass]
    public class GetLuckyNumbersFromMockData
    {
        #region Properties
        private readonly List<LuckyMoneyNumbers> _LuckyMoneyMockData;
        #endregion

        #region Constructor
        public GetLuckyNumbersFromMockData()
        {
            _LuckyMoneyMockData = new List<LuckyMoneyNumbers>();
            var service = new FloridaLotteryLuckMoneyHTMLService(new MockLuckyMoneyRepository());
            _LuckyMoneyMockData = service.GetParsedHistoryResults();
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void ShouldGetListWithCountOf10()
        {
            Assert.IsTrue(_LuckyMoneyMockData.Count == 10);
        }

        [TestMethod]
        public void ShouldGetListWithPositionsAsc()
        {
            foreach (LuckyMoneyNumbers lmn in _LuckyMoneyMockData)
            {
                Assert.IsTrue(lmn.Num1 < lmn.Num2);
                Assert.IsTrue(lmn.Num2 < lmn.Num3);
                Assert.IsTrue(lmn.Num3 < lmn.Num4);
            }
        }
        #endregion
    }
}