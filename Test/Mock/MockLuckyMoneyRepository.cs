using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using Business;

namespace Test.Mock
{
    class MockLuckyMoneyRepository : Checks, IFloridaLotteryLuckMoneyHTMLRepository
    {
        private readonly List<LuckyMoneyNumbers> _LuckyMoneyMockData;

        private bool _SiteAvailable;

        public MockLuckyMoneyRepository()
        {
            _LuckyMoneyMockData = new List<LuckyMoneyNumbers>();
        }

        public List<LuckyMoneyNumbers> GetParsedHistoryResults()
        {
            GetLuckyMoneyNumbersFromMock();
            var LMN = _LuckyMoneyMockData;
            return LMN;
        }

        public bool SiteAvailable()
        {
            GetSiteIsUp();
            var GSIU = _SiteAvailable;
            return GSIU;
        }

        public void GetLuckyMoneyNumbersFromMock()
        {
            for (int i = 0; i < 10; i++)
            {
                LuckyMoneyNumbers lmn = new LuckyMoneyNumbers();
                lmn.Num1 = GenerateRandom(1);
                lmn.Num2 = GenerateRandom(2);
                lmn.Num3 = GenerateRandom(3);
                lmn.Num4 = GenerateRandom(4);
                lmn.LB = GenerateRandom(5);
                lmn.WinDate = DateTime.Parse(DateTime.Now.AddDays(i).ToShortDateString());

                _LuckyMoneyMockData.Add(lmn);
            }

        }

        public void GetSiteIsUp()
        {
            _SiteAvailable = true;
        }

        public int GenerateRandom(int pos)
        {
            Random random = new Random();
            int randomNumber = 0;
            switch (pos)
            {
                case 1:
                    randomNumber = random.Next(1, 12);
                    break;
                case 2:
                    randomNumber = random.Next(13, 24);
                    break;
                case 3:
                    randomNumber = random.Next(25, 37);
                    break;
                case 4:
                    randomNumber = random.Next(38, 47);
                    break;
                case 5:
                    randomNumber = random.Next(1, 17);
                    break;
            }


            return randomNumber;
        }

    }
}

