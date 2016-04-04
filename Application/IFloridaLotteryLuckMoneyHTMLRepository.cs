using System;
using System.Collections.Generic;
using Business;

namespace App
{
    public interface IFloridaLotteryLuckMoneyHTMLRepository
    {
        List<LuckyMoneyNumbers> GetParsedHistoryResults();
        bool SiteAvailable();
    }
}
