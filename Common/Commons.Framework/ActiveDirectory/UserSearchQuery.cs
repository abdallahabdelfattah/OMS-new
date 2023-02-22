using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.ActiveDirectory
{
    public class UserSearchQuery
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Department { get; set; }

        public string Mobile { get; set; }

        public bool IsGetAllUsers => string.IsNullOrEmpty(this.UserName) && string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(this.Email)
                                     && string.IsNullOrEmpty(this.Department) && string.IsNullOrEmpty(this.Job)
                                     && string.IsNullOrEmpty(this.Mobile);

        public string Job { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
