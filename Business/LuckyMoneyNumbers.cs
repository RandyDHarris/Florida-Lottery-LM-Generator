//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        LuckyMoneyNumbers
//Description:  This struct is a data structure that represents a Lucky Money drawing. 

#region Assemblies
using System;
#endregion

namespace Business
{
    public struct LuckyMoneyNumbers
    {
        #region Properties
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Num3 { get; set; }
        public int Num4 { get; set; }
        public int LB { get; set; }
        public DateTime WinDate { get; set; }
        #endregion
    }
}