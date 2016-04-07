using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using Business;

namespace Test.Mock
{
    class MockLuckyMoneyRepository : Checks, ILuckyMoneyNumbersRepository
    {
        private readonly List<LuckyMoneyNumbers> _LuckyMoneyMockData;

        public MockLuckyMoneyRepository()
        {
            _LuckyMoneyMockData = new List<LuckyMoneyNumbers>();
        }

        public List<LuckyMoneyNumbers> GetLuckyMoneyNumbers()
        {
            GetLuckyMoneyNumbersFromMock();
            var LMN = _LuckyMoneyMockData;
            return LMN;
        }

        public void InsertLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
        {
            CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
        }

        public void UpdateLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
        {
            //CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
        }

        public void DeleteLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
        {
            CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
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

        public void CUDHistory(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
        {
        }
    }
}

