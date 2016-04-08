//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        IFloridaLotteryLuckMoneyHTMLRepository
//Description:  This is the interface to the class FloridaLotteryLuckMoneyHTMLRepository.

#region Assemblies
using System;
using System.Collections.Generic;
using Business;
#endregion

namespace App
{
    #region Methods
    public interface IFloridaLotteryLuckMoneyHTMLRepository
    {
        List<LuckyMoneyNumbers> GetParsedHistoryResults();
        bool SiteAvailable();
    }
    #endregion
}
