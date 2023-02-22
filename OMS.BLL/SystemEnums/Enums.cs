using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL.SystemEnums
{
    public enum LanguageEnum : int
    {
        English = 1,
        Arabic = 2
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
}
