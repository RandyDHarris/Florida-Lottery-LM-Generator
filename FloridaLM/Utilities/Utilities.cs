using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;

namespace FloridaLM
{
    public class Utilities : FloridaLM.IUtilities.ICheckInt, FloridaLM.IUtilities.ICheckDate, FloridaLM.IUtilities.ICheckIntReturnZero
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

    }
}