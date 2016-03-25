using System;
namespace FloridaLM.IDataAccess
{
    interface ICUDHistory
    {
        void CUDHistory(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate);
    }
}
