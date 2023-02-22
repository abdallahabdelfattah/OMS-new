using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.SystemEnums
{
    public enum LanguageEnum : int
    {
        English = 1,
        Arabic = 2
    }

    public enum GenderEnum : int
    {
        Male = 1,
        Female = 2
    }

    public enum EnumErrorStatus : int
    {
        New = 1,
        Sent = 2
    }

    public enum UserStatus : int
    {
        Active = 1,
        InActive = 2
    }

    public enum PageActionMode : int
    {
        FromGroup = 1,
        Yes = 2,
        No = 3
    }

    public enum WarehouseTransactionEnum : int
    {
        Add = 1,
        Subtract = 2,
        Return = 3,
        Dispose = 4,
        TransferFom = 5,
        TransferTo = 6,
    }
    public enum SalesTransactionEnum : int
    {
        SalesRequest = 1,
        SalesInvoice = 2,
        CanceledRequest = 3,
        SalesReturn = 4,
    }

    public enum EmailsReceivers: int
    {
        All = 0,
        Doctors = 1,
        Customers = 2,
        Employees = 3,
        Suppliers = 4
    }
    public enum StatusEnum : int
    {
        Pending = 1,
        Approved = 2,
        Canceled = 3,
    }
    public enum OperationTypeEnum : int
    {
        Plus = 1,
        Subtract = 2,
        PlusPercentage = 3,
        SubtractPercentage = 4,
    }
    public enum EditProductColumnEnum : int
    {
        price = 1,
        Quantity = 2,

    }
    public enum ProductCategories : int
    {
        MedicalGlasses =1,	
        sunglasses =2,	
        ClepOn =3,	
        Reading =4,	
        CL= 5,	
        CLSP= 6,	
        CLSolution= 7,	
        Others =8,	
        OpticalLenses =9,	
        Service = 10

    }
}
