//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        Checks
//Description:  This class contains helper methods that check for invalid values     

#region Assemblies
using System;
#endregion

namespace Business
{
    public class Checks
    {
        #region Helper Methods
        public bool CheckDate(string inval)
        {
            bool retval = false;

            DateTime dateValue;

            if (DateTime.TryParse(inval, out dateValue))
            {
                retval = true;
            }

            return retval;
        }
        public bool CheckInt(string inval)
        {
            bool retval = false;

            int dateValue;

            if (int.TryParse(inval, out dateValue))
            {
                retval = true;
            }

            return retval;
        }
        public int CheckIntReturnZero(object obj)
        {
            int number;

            bool result = Int32.TryParse(obj.ToString(), out number);

            if (!result)
            {
                return 0;
            }
            else
            {
                return number;
            }
        }
        public DateTime CheckDateReturnMin(object obj)
        {
            if (obj != null)
            {
                DateTime _date;

                bool result = DateTime.TryParse(obj.ToString(), out _date);

                if (!result)
                {
                    return DateTime.MinValue.Date;
                }
                else
                {
                    return _date;
                }
            }
            else 
            {
                return DateTime.MinValue.Date;
            }
        }
        #endregion
    }
}
