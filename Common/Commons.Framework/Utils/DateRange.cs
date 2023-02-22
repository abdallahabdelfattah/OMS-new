using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Utils
{
    public class DateRange
    {
        public DateRange()
        {
            
        }
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double TotalDays { get { return (EndDate - StartDate).TotalDays; } }

        public bool Includes(DateTime value)
        {
            return (StartDate <= value) && (value <= EndDate);
        }
    }
}
