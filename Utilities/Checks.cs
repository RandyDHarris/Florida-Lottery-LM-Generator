using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Checks
    {
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
    }
}
