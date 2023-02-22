using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Resources;

namespace OMS.BLL.Utilities
{
    public class BusinessException : Exception
    {
        public List<string> Exceptions { get; set; }

        public BusinessException()
        {
            Exceptions = new List<string>();
        }

        public void AddRequiredMessage(string FieldName)
        {
            Exceptions.Add(string.Format(AppResource.IsRequiredFormat, FieldName));
        }
        public void AddDecimal(string FieldName)
        {
            Exceptions.Add(string.Format("IsDecimalFormat", FieldName));
        }

        public void AddGreaterToZero(string FieldName)
        {
            Exceptions.Add(string.Format("AddGreaterToZero", FieldName));
        }
        public void AddInteger(string FieldName)
        {
            Exceptions.Add(string.Format("NumericOnlyFormat", FieldName));
        }
        public void AddExistsMessage(string value)
        {
            Exceptions.Add(string.Format(AppResource.AlreadyExistsFormat, value));
        }
        public void AddCompare(string value1, string value2)
        {
            Exceptions.Add(string.Format("CompareFormat", value1, value2));
        }
        public void AddGreater(string value1, string value2)
        {
            Exceptions.Add(string.Format("GreaterThanFormat", value1, value2));
        }
        public void AddNotValid(string FieldName)
        {
            Exceptions.Add(string.Format("NotValidFormat", FieldName));
        }
        public void AddQtyNotExists(string FieldName)
        {
            Exceptions.Add(string.Format("الكمية المطلوبة غير متوفرة بالمخزن", FieldName));
        }

        internal void MaxChild(string FieldName)
        {
            Exceptions.Add(FieldName + " can't be fourth level  ");
        }
        internal void NotValid(string value)
        {
            Exceptions.Add(value);
        }

        internal void AddCannotDeleteBecause(string FieldName)
        {
            Exceptions.Add(AppResource.CanNotDelete);
        }

        public void StringMessage(string message)
        {
            Exceptions.Add(message);
        }
    }
}
