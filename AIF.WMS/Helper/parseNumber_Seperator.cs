using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AIF.WMS.HelperClass
{
    public class parseNumber_Seperator
    {
        public static double ConvertToDouble(string val)
        {
 
            string decimalSembol = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string digitGrouping = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator;
            if (val.Contains(decimalSembol) && val.Contains(digitGrouping))
            {
                val = val.Replace(".", "");
                //val = val.Replace(digitGrouping, "");
            }

            val = val.Replace(digitGrouping, decimalSembol);

            double ret = Convert.ToDouble(val);

            return ret;
        }

        public static string setDoubleVal(string val)
        {
            string decimalSembol = Program.decimalSeperator;
            string digitGrouping = Program.thousandsSeperator;

            val = val.Replace(decimalSembol, digitGrouping);

            return val;
        }
    }
}
