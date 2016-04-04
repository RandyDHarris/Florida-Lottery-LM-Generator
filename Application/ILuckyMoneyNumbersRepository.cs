using System;
using System.Collections.Generic;
using Business;

namespace App
{
    public interface ILuckyMoneyNumbersRepository
    {
        List<LuckyMoneyNumbers> GetLuckyMoneyNumbers();
        void InsertLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate);
        void UpdateLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate);
        void DeleteLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate);
    }
}
