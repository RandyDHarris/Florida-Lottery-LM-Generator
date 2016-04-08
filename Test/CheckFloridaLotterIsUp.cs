//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        CheckFloridaLotterIsUp
//Description:  This is a test class that uses mock data to execute 
//              the test that determines if the site "FloridaLottery.com" is up.

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
    public class CheckFloridaLotterIsUp
    {
        #region Test Methods
        [TestMethod]
        public void SiteShouldBeUp()
        {
            bool _SiteIsUp = false;
            var service = new FloridaLotteryLuckMoneyHTMLService(new MockLuckyMoneyRepository());
            _SiteIsUp = service.GetSiteIsUp();
            Assert.IsTrue(_SiteIsUp);
        }
        #endregion
    }
}
