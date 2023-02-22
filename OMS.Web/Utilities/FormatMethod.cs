using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Web.Utilities
{
    public static class FormatMethod
    {
        public static decimal? DoFormat(decimal value)
        {
            var S = String.Format("{0:0.00}", value);
            if (value == 0)
            {
                return 0;
            }
            if (S.EndsWith("00"))
            {
                return decimal.Parse(value.ToString("##,#"));
            }
            else
            {
                return value;
            }
        }
    }
}